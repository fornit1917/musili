import Settings from "./settings";

export default class Player {    
    private root: HTMLElement;
    private playPauseBtn: HTMLElement;
    private playIcon: HTMLElement;
    private pauseIcon: HTMLElement;
    private nextBtn: HTMLElement;

    private isPaused: boolean = true;

    constructor(selector: string, settings: Settings) {
        this.root = document.querySelector(selector);
        this.playPauseBtn = this.root.querySelector(".js-play");
        this.playIcon = this.playPauseBtn.querySelector(".js-icon-play");
        this.pauseIcon = this.playPauseBtn.querySelector(".js-icon-pause");
        this.nextBtn = this.root.querySelector(".js-next");

        this.playPauseBtn.addEventListener("click", e => { this.onPlayPauseClick(e); });
        this.nextBtn.addEventListener("click", e => { this.onNextClick(e); });
    }

    private onPlayPauseClick(e: MouseEvent) {
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
        console.log("Next!");
    }
}