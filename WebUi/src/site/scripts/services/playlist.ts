import ApiClient from "./api-client";
import { TracksCriteria, Track } from "../dto-types";

export default class Playlist {
    private apiClient: ApiClient;
    private tracks: Track[];
    private tracksCriteria: TracksCriteria = null;

    public constructor(apiClient: ApiClient) {
        this.apiClient = apiClient;
        this.tracks = [];
    }

    public loadTracks(tracksCriteria: TracksCriteria | null): Promise<void> {
        if (tracksCriteria === null && this.tracksCriteria === null) {
            throw new Error("Tracks criteria must be specified");
        }
        this.tracksCriteria = tracksCriteria;
        return this.apiClient.getTracks(this.tracksCriteria)
            .then(tracks => {
                this.tracks = this.tracks.concat(tracks);
            });
    }

    public getNextTrack(): Track | null {
        return this.tracks.length > 0 ? this.tracks[0] : null;
    }
}