import { Track } from "../dto-types";

const TRACK_ID_KEY = "msl_last_track_id";
const TIMESTAMP_KEY = "msl_last_track_timestamp";
const MAX_AGE = 3600 * 1000;

export default class TracksHistoryStorage {
    public getLastTrackId(): number {
        const lastId = Number(localStorage.getItem(TRACK_ID_KEY));
        const ts = Number(localStorage.getItem(TIMESTAMP_KEY));
        if (lastId && Date.now() - ts <= MAX_AGE) {
            return lastId;
        }
        return 0;
    }

    public saveLastTrackId(tracks: Track[]): void {
        localStorage.setItem(TRACK_ID_KEY, String(getMaxId(tracks)));
        localStorage.setItem(TIMESTAMP_KEY, String(Date.now()));
    }
}

function getMaxId(tracks: Track[]): number {
    let maxId: number = 0;
    for (let i = 0; i < tracks.length; i++) {
        if (tracks[i].id > maxId) {
            maxId = tracks[i].id;
        }
    }
    return maxId;
}