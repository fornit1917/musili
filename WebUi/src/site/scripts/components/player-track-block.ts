import { Track } from "../dto-types";

export default class PlayerTrackBlock {
    private spinner: HTMLElement;
    private trackInfo: HTMLElement;

    public constructor (root: HTMLElement) {
        this.spinner = root.querySelector(".js-spinner");
        this.trackInfo = root.querySelector(".js-track-info");
    }

    public showLoadingIndicator() {
        this.trackInfo.style.display = "none";
        this.spinner.style.display = "block";
    }

    public showTrackInfo(track: Track) {
        this.trackInfo.style.display = "block";
        this.spinner.style.display = "none";
    }
}