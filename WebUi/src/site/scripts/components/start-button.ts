export default class StartButton {
    private onStart: () => void;
    private startBtn: HTMLElement;

    constructor(selector: string, onStart: () => void) {
        this.onStart = onStart;
        this.startBtn = document.querySelector(selector);
        this.startBtn.addEventListener("click", e => { this.onStartBtnClick(e); });
    }

    private onStartBtnClick(e: Event) {
        this.startBtn.style.display = "none";
        this.onStart();
    }
}