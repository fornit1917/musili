import { TracksCriteria, Track } from "../dto-types";

export default class ApiClient {
    getTracks(tracksCriteria: TracksCriteria, lastId: number): Promise<Track[]> {
        const url = `/api/tracks?tempos=${tracksCriteria.tempo != "any" ? tracksCriteria.tempo : ""}&genres=${tracksCriteria.genres.join(",")}&lastId=${lastId}`;
        return fetch(url)
            .then(response => response.json())
            .then(items => items.map((item: Track) => {
                return { ...item, localExpirationTime: Date.now() + item.remainingLifeSeconds * 1000 };
            }));
    }
}