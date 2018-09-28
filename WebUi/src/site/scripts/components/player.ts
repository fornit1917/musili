import { TracksCriteria } from "../dto-types";
import PlayerSettingsButton from "./player-settings-button";
import PlayerControls from "./player-controls";

interface PlayerMessageHandlers {
    onShowSettings: () => void;
    onHideSettings: () => void;
}

export default class Player {
    private root: HTMLElement;

    private settingsBtn: PlayerSettingsButton;
    private controls: PlayerControls;
    
    private applySettingsTimeoutId: number = 0;

    private messageHandlers: PlayerMessageHandlers;

    constructor(selector: string, messageHandlers: PlayerMessageHandlers) {
        this.messageHandlers = messageHandlers;
        this.root = document.querySelector(selector);

        this.settingsBtn = new PlayerSettingsButton(this.root, this.messageHandlers);
        this.controls = new PlayerControls(this.root, {
            onNext: () => { this.onNext(); },
            onPlay: () => { this.onPlay(); },
            onPause: () => { this.onPause(); },
        });
        
        this.setDisabled(true);
    }

    public loadAndStart(settings: TracksCriteria) {
        console.log("Start with settings!");
        console.log(settings);
    }

    public applyTracksSettingsAfterTimeout(settings: TracksCriteria) {
        this.settingsBtn.resetAutoHideTimeout();

        if (this.applySettingsTimeoutId) {
            clearTimeout(this.applySettingsTimeoutId);
        }
        this.applySettingsTimeoutId = setTimeout(() => {
            console.log("Settings was changed!");
            console.log(settings);
        }, 1500);
    }

    private setDisabled(isDisabled: boolean) {
        this.settingsBtn.setDisabled(isDisabled);
        this.controls.setDisabled(isDisabled);
    }

    private onPlay() {
        console.log("Play!");
    }

    private onPause() {
        console.log("Pause!");
    }

    private onNext() {
        console.log("Next!");
    }
}