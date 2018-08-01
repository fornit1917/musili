import Settings from "./settings";
import { nodeListToArray } from "../utils/dom-utils";

export default class Player {    
    private root: HTMLElement;
    private playPauseBtn: HTMLElement;
    private playIcon: HTMLElement;
    private pauseIcon: HTMLElement;
    private nextBtn: HTMLElement;
    private settingsBtn: HTMLElement;
    private settings: Settings;

    private isPaused: boolean = true;
    private isDisabled: boolean = true;

    constructor(selector: string, settings: Settings) {
        this.settings = settings;

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

    private setDisabled(isDisabled: boolean) {
        this.isDisabled = isDisabled;
        nodeListToArray(this.root.querySelectorAll("button")).forEach(item => isDisabled ? item.classList.add("disabled") : item.classList.remove("disabled"));
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
        if (this.settings.isHidden) {
            this.settings.show();
            btn.innerText = btn.dataset.textHide;
        } else {
            this.settings.hide();
            btn.innerText = btn.dataset.text;
        }
    }
}