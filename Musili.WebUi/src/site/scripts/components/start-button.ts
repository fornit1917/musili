import Player from "./player";
import Settings from "./settings";
import Bg from "./bg";

export default class StartButton {
    private startBtn: HTMLElement;
    private player: Player;
    private settings: Settings;
    private bg: Bg;

    constructor(selector: string, player: Player, settings: Settings, bg: Bg) {
        this.player = player;
        this.settings = settings;
        this.startBtn = document.querySelector(selector);
        this.startBtn.addEventListener("click", e => { this.onStartBtnClick(e); });
        this.bg = bg;
    }

    private onStartBtnClick(e: MouseEvent) {
        this.startBtn.style.display = "none";
        this.settings.hide();
        this.bg.show();
        this.player.loadAndStart();
    }
}