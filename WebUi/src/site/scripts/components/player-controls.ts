interface MessageHandlers {
    onPlay: () => void;
    onPause: () => void;
    onNext: () => void;
}

export default class PlayerControls {
    private messageHandlers: MessageHandlers;

    private playPauseBtn: HTMLElement;
    private playIcon: HTMLElement;
    private pauseIcon: HTMLElement;
    private nextBtn: HTMLElement;

    private isDisabled: boolean = true;
    private isPaused: boolean = true;
    
    public constructor(root: HTMLElement, messageHandlers: MessageHandlers) {
        this.messageHandlers = messageHandlers;

        this.playPauseBtn = root.querySelector(".js-play");
        this.playIcon = this.playPauseBtn.querySelector(".js-icon-play");
        this.pauseIcon = this.playPauseBtn.querySelector(".js-icon-pause");
        this.nextBtn = root.querySelector(".js-next");

        this.playPauseBtn.addEventListener("click", () => { this.onPlayPauseClick(); });
        this.nextBtn.addEventListener("click", () => { this.onNextClick(); });        
    }

    public setDisabled(isDisabled: boolean) {
        this.isDisabled = isDisabled;
        this.playPauseBtn.classList.toggle("disabled", isDisabled);
        this.nextBtn.classList.toggle("disabled", isDisabled);
    }

    public forcePlay() {
        this.isPaused = false;
        this.updateIcons();
    }

    private updateIcons() {
        if (this.isPaused) {
            this.pauseIcon.style.display = "none";
            this.playIcon.style.display = "block";
        } else {
            this.pauseIcon.style.display = "block";
            this.playIcon.style.display = "none";            
        }
    }

    private onPlayPauseClick() {
        if (this.isDisabled) {
            return;
        }

        this.isPaused = !this.isPaused;
        this.updateIcons();
        if (this.isPaused) {
            this.messageHandlers.onPause();
        } else {
            this.messageHandlers.onPlay();
        }
    }

    private onNextClick() {
        if (this.isDisabled) {
            return;
        }

        this.messageHandlers.onNext();
    }
}