import { TracksCriteria, Track } from "../dto-types";
import PlayerSettingsButton from "./player-settings-button";
import PlayerControls from "./player-controls";
import PlayerTrackBlock from "./player-track-block";
import ApiClient from "../services/api-client";
import Playlist from "../services/playlist";

interface PlayerMessageHandlers {
    onShowSettings: () => void;
    onHideSettings: () => void;
}

export default class Player {
    private root: HTMLElement;

    private settingsBtn: PlayerSettingsButton;
    private controls: PlayerControls;
    private trackBlock: PlayerTrackBlock;

    private playlist: Playlist;
    
    private applySettingsTimeoutId: number = 0;

    private messageHandlers: PlayerMessageHandlers;

    constructor(selector: string, apiClient: ApiClient, messageHandlers: PlayerMessageHandlers) {
        this.messageHandlers = messageHandlers;
        this.root = document.querySelector(selector);
        

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
                console.log(e);
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
    }

    private startLoading() {
        this.setDisabled(true);
        this.trackBlock.showLoadingIndicator();
    }

    private playTrack(track: Track) {
        console.log(track);
        this.setDisabled(false);
        this.trackBlock.showTrackInfo(track);
        this.controls.forcePlay();
    }    

    private onPlay() {
        console.log("Play!");
    }

    private onPause() {
        console.log("Pause!");
    }

    private onNext() {
        const track = this.playlist.getNextTrack();
        if (track === null) {
            this.loadAndStart();
        } else {
            this.playTrack(track);
        }
        console.log("Next!");
    }
}