import Player from "./player";
import Settings from "./settings";

export default class StartButton {
    private startBtn: HTMLElement;
    private player: Player;
    private settings: Settings;

    constructor(selector: string, player: Player, settings: Settings) {
        this.player = player;
        this.settings = settings;
        this.startBtn = document.querySelector(selector);
        this.startBtn.addEventListener("click", e => { this.onStartBtnClick(e); });
    }

    private onStartBtnClick(e: MouseEvent) {
        this.startBtn.style.display = "none";
        this.settings.hide();
    }
}