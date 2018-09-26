import { nodeListToArray } from "../utils/dom-utils";
import { TracksCriteria } from "../services/types";

interface PlayerMessageHandlers {
    onShowSettings: () => void;
    onHideSettings: () => void;
}

export default class Player {
    private root: HTMLElement;
    private playPauseBtn: HTMLElement;
    private playIcon: HTMLElement;
    private pauseIcon: HTMLElement;
    private nextBtn: HTMLElement;

    private settingsBtn: HTMLElement;
    private isSettingsHidden: boolean = true;

    private isPaused: boolean = true;
    private isDisabled: boolean = true;
    private hideSettingsTimeoutId: number = 0;
    private applySettingsTimeoutId: number = 0;

    private messageHandlers: PlayerMessageHandlers;

    constructor(selector: string, messageHandlers: PlayerMessageHandlers) {
        this.messageHandlers = messageHandlers;

        this.root = document.querySelector(selector);
        this.playPauseBtn = this.root.querySelector(".js-play");
        this.playIcon = this.playPauseBtn.querySelector(".js-icon-play");
        this.pauseIcon = this.playPauseBtn.querySelector(".js-icon-pause");
        this.nextBtn = this.root.querySelector(".js-next");
        this.settingsBtn = this.root.querySelector(".js-settings");

        this.playPauseBtn.addEventListener("click", e => { this.onPlayPauseClick(e); });
        this.nextBtn.addEventListener("click", e => { this.onNextClick(e); });
        this.settingsBtn.addEventListener("click", e => { this.onSettingsClick(e); });
        
        this.setDisabled(true);
    }

    public loadAndStart() {
        this.setDisabled(false);
    }

    public applyTracksSettingsAfterTimeout(settings: TracksCriteria) {
        this.hideSettingsAfterTimeout();

        if (this.applySettingsTimeoutId) {
            clearTimeout(this.applySettingsTimeoutId);
        }
        this.applySettingsTimeoutId = setTimeout(() => {
            console.log("Settings was changed!");
            console.log(settings);
        }, 1500);
    }

    private hideSettingsAfterTimeout() {
        if (this.hideSettingsTimeoutId) {
            clearTimeout(this.hideSettingsTimeoutId);
        }
        this.hideSettingsTimeoutId = setTimeout(() => { this.hideSettings(); }, 10000);
    }

    private setDisabled(isDisabled: boolean) {
        this.isDisabled = isDisabled;
        nodeListToArray(this.root.querySelectorAll("button")).forEach(item => isDisabled ? item.classList.add("disabled") : item.classList.remove("disabled"));
    }

    private hideSettings() {
        const btn = this.settingsBtn;
        const icon = btn.querySelector(".js-icon") as HTMLElement;
        const text = btn.querySelector(".js-text") as HTMLElement;
        text.innerText = btn.dataset.text;
        icon.style.display = "block";

        if (this.hideSettingsTimeoutId) {
            clearTimeout(this.hideSettingsTimeoutId);
            this.hideSettingsTimeoutId = 0;
        }

        this.messageHandlers.onHideSettings();
        this.isSettingsHidden = true;
    }

    private onPlayPauseClick(e: MouseEvent) {
        if (this.isDisabled) {
            return;
        }

        this.isPaused = !this.isPaused;
        if (this.isPaused) {
            this.pauseIcon.style.display = "none";
            this.playIcon.style.display = "block";
        } else {
            this.pauseIcon.style.display = "block";
            this.playIcon.style.display = "none";
        }
    }

    private onNextClick(e: MouseEvent) {
        if (this.isDisabled) {
            return;
        }

        console.log("Next!");
    }

    private onSettingsClick(e: MouseEvent) {
        if (this.isDisabled) {
            return;
        }

        const btn = this.settingsBtn;
        const icon = btn.querySelector(".js-icon") as HTMLElement;
        const text = btn.querySelector(".js-text") as HTMLElement;
        if (this.isSettingsHidden) {
            this.messageHandlers.onShowSettings();
            this.isSettingsHidden = false;
            text.innerText = btn.dataset.textHide;
            icon.style.display = "none";
            this.hideSettingsAfterTimeout();
        } else {
            this.hideSettings();
        }
    }
}