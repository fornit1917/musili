import { TracksCriteria, Track } from "../dto-types";
import PlayerSettingsButton from "./player-settings-button";
import PlayerControls from "./player-controls";
import PlayerTrackBlock from "./player-track-block";
import ApiClient from "../services/api-client";
import Playlist from "../services/playlist";
import { formatSeconds } from "../utils/time-utils";

interface PlayerMessageHandlers {
    onShowSettings: () => Promise<void>;
    onHideSettings: () => Promise<void>;
}

export default class Player {
    private root: HTMLElement;
    private audio: HTMLAudioElement;

    private progress: HTMLElement;
    private progressBg: HTMLElement;
    private timeCurrent: HTMLElement;
    private timeTotal: HTMLElement;
    
    
    private settingsBtn: PlayerSettingsButton;
    private controls: PlayerControls;
    private trackBlock: PlayerTrackBlock;
    
    private playlist: Playlist;
    
    private applySettingsTimeoutId: number = 0;
    private isDisabled: boolean = false;

    private messageHandlers: PlayerMessageHandlers;

    constructor(selector: string, apiClient: ApiClient, messageHandlers: PlayerMessageHandlers) {
        this.messageHandlers = messageHandlers;
        this.root = document.querySelector(selector);

        this.progress = this.root.querySelector(".js-progress");
        this.progressBg = this.root.querySelector(".js-progress-bg");
        this.timeCurrent = this.root.querySelector(".js-time-current");
        this.timeTotal = this.root.querySelector(".js-time-total");
        this.progress.onclick = e => { this.onProgressBarClick(e); }

        this.audio = this.root.querySelector(".js-audio");
        this.audio.onended = () => { this.onNext(); };
        this.audio.onerror = () => { this.onNext(); };
        this.audio.ontimeupdate = () => { this.onProgress(); }
        this.audio.onloadedmetadata = () => { this.onLoadMetadata() }

        this.settingsBtn = new PlayerSettingsButton(this.root, this.messageHandlers);
        this.controls = new PlayerControls(this.root, {
            onNext: () => { this.onNext(); },
            onPlay: () => { this.onPlay(); },
            onPause: () => { this.onPause(); },
        });
        this.trackBlock = new PlayerTrackBlock(this.root);

        this.playlist = new Playlist(apiClient);

        this.setDisabled(true);
    }

    public loadAndStart(tracksCriteria: TracksCriteria | null = null) {
        this.startLoading();
        this.playlist.loadTracks(tracksCriteria)
            .then(() => {
                const track = this.playlist.getNextTrack();
                if (track !== null) {
                    this.playTrack(track);
                } else {
                    throw new Error();
                }
            })
            .catch(e => {
                alert("Tracks loading error");
                location.reload();
            });
    }

    public applyTracksSettingsAfterTimeout(settings: TracksCriteria) {
        this.settingsBtn.resetAutoHideTimeout();

        if (this.applySettingsTimeoutId) {
            clearTimeout(this.applySettingsTimeoutId);
        }
        this.applySettingsTimeoutId = setTimeout(() => {
            this.loadAndStart(settings);
        }, 1500);
    }

    private setDisabled(isDisabled: boolean) {
        this.settingsBtn.setDisabled(isDisabled);
        this.controls.setDisabled(isDisabled);
        this.isDisabled = isDisabled;
    }

    private startLoading() {
        this.setDisabled(true);
        this.trackBlock.showLoadingIndicator();
    }

    private playTrack(track: Track) {
        this.setDisabled(false);
        this.trackBlock.showTrackInfo(track);
        this.audio.src = track.url;
        this.audio.play();
        this.controls.forcePlay();
    }    

    private onPlay() {
        this.audio.play();
    }

    private onPause() {
        this.audio.pause();
    }

    private onNext() {
        const track = this.playlist.getNextTrack();
        if (track === null) {
            this.loadAndStart();
        } else {
            this.playTrack(track);
        }
    }

    private onLoadMetadata() {
        this.timeTotal.innerText = formatSeconds(this.audio.duration);
    }

    private onProgress() {
        const percent = (this.audio.currentTime / this.audio.duration) * 100;
        this.progressBg.style.width = `${percent}%`;
        this.timeCurrent.innerText = formatSeconds(this.audio.currentTime);
    }

    private onProgressBarClick(e: MouseEvent) {
        if (this.isDisabled || !this.audio.duration) {
            return;
        }

        const position = e.clientX / this.progress.offsetWidth;
        const time = position * this.audio.duration;
        this.audio.currentTime = time;
    }
}