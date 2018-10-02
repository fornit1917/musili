import { Track } from "../dto-types";

export default class PlayerTrackBlock {
    private spinner: HTMLElement;
    private trackInfo: HTMLElement;
    private artist: HTMLElement;
    private title: HTMLElement;

    public constructor (root: HTMLElement) {
        this.spinner = root.querySelector(".js-spinner");
        this.trackInfo = root.querySelector(".js-track-info");
        this.artist = this.trackInfo.querySelector(".js-artist");
        this.title = this.trackInfo.querySelector(".js-title");
    }

    public showLoadingIndicator() {
        this.trackInfo.style.display = "none";
        this.spinner.style.display = "block";
    }

    public showTrackInfo(track: Track) {
        this.trackInfo.style.display = "block";
        this.spinner.style.display = "none";
        this.artist.innerHTML = track.artist;
        this.title.innerHTML = track.title;
    }
}