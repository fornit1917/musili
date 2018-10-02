import ApiClient from "./api-client";
import { TracksCriteria, Track } from "../dto-types";

export default class Playlist {
    private apiClient: ApiClient;
    private tracks: Track[];
    private index: number = -1;
    private loadingPromise: Promise<void> | null = null;
    private tracksCriteria: TracksCriteria = null;

    public constructor(apiClient: ApiClient) {
        this.apiClient = apiClient;
        this.tracks = [];
    }

    public loadTracks(tracksCriteria: TracksCriteria | null = null): Promise<void> {
        if (tracksCriteria === null && this.tracksCriteria === null) {
            throw new Error("Tracks criteria must be specified");
        }

        if (tracksCriteria !== null) {
            this.tracksCriteria = tracksCriteria;
            this.index = -1;
            this.tracks = [];
        }

        if (this.loadingPromise && tracksCriteria !== null) {
            return this.loadingPromise;
        }

        const previous = this.loadingPromise ? this.loadingPromise : Promise.resolve();
        this.loadingPromise = previous.then(() => this.apiClient.getTracks(this.tracksCriteria)) 
            .then(tracks => {
                this.tracks = this.tracks.concat(tracks);
                this.loadingPromise = null;
            })
            .catch(e => {
                this.loadingPromise = null;
                throw new Error(e);
            });

        return this.loadingPromise;
    }

    public getNextTrack(): Track | null {
        this.index++;
        const time = Date.now();
        while (this.index < this.tracks.length && this.tracks[this.index].localExpirationTime <= time) {
            this.index++;
        }

        if (this.index < this.tracks.length) {
            if (this.index === this.tracks.length-1) {
                this.loadTracks();
            }
            return this.tracks[this.index];
        }

        return null;
    }
}

