import { TracksCriteria, Track } from "../dto-types";

export default class ApiClient {
    getTracks(tracksCriteria: TracksCriteria): Promise<Track[]> {
        return new Promise<Track[]>(resolve => {
            setTimeout(() => {
                resolve([
                    { title: "Some track title", artist: "Some srtist", url: "" },
                    { title: "Some track title", artist: "Some srtist", url: "" },
                ])
            }, 2000);
        });
    }
}