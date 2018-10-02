import { TracksCriteria, Track } from "../dto-types";

export default class ApiClient {
    getTracks(tracksCriteria: TracksCriteria): Promise<Track[]> {
        const url = `/api/tracks?tempos=${tracksCriteria.tempo}&genres=${tracksCriteria.genres.join(",")}`;
        return fetch(url)
            .then(response => response.json())
            .then(items => items.map((item: Track) => {
                return { ...item, localExpirationTime: Date.now() + item.remainingLifeSeconds * 1000 };
            }));
    }
}