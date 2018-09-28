interface MessageHandlers {
    onShowSettings: () => void;
    onHideSettings: () => void;
}

export default class PlayerSettingsButton {
    private btn: HTMLElement;
    private icon: HTMLElement;
    private text: HTMLElement;

    private isDisabled: boolean;
    private isSettingsHidden: boolean = true;
    private hideSettingsTimeoutId: number = 0;

    private messageHandlers: MessageHandlers;

    public constructor(root: HTMLElement, messageHandlers: MessageHandlers) {
        this.messageHandlers = messageHandlers;

        this.btn = root.querySelector(".js-settings");
        this.icon = this.btn.querySelector(".js-icon") as HTMLElement;
        this.text = this.btn.querySelector(".js-text") as HTMLElement;
        
        this.btn.addEventListener("click", () => { this.onClick(); });
    }

    public setDisabled(isDisabled: boolean) {
        this.isDisabled = isDisabled;
        this.btn.classList.toggle("disabled", isDisabled);
    }

    public resetAutoHideTimeout() {
        if (this.hideSettingsTimeoutId) {
            this.hideSettingsAfterTimeout();
        }
    }

    private onClick() {
        if (this.isDisabled) {
            return;
        }
        if (this.isSettingsHidden) {
            this.showSettings();
        } else {
            this.hideSettings();
        }        
    }

    private showSettings() {
        this.messageHandlers.onShowSettings();
        this.isSettingsHidden = false;
        this.text.innerText = this.btn.dataset.textHide;
        this.icon.style.display = "none";
        this.hideSettingsAfterTimeout();
    }

    private hideSettings() {
        this.text.innerText = this.btn.dataset.text;
        this.icon.style.display = "block";

        if (this.hideSettingsTimeoutId) {
            clearTimeout(this.hideSettingsTimeoutId);
            this.hideSettingsTimeoutId = 0;
        }

        this.messageHandlers.onHideSettings();
        this.isSettingsHidden = true;
    }

    private hideSettingsAfterTimeout() {
        if (this.hideSettingsTimeoutId) {
            clearTimeout(this.hideSettingsTimeoutId);
        }
        this.hideSettingsTimeoutId = setTimeout(() => { this.hideSettings(); }, 10000);
    }
}