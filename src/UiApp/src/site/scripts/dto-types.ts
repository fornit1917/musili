export interface TracksCriteria {
    tempo: string;
    genres: string[];
}

export interface Track {
    id: number;
    artist: string;
    title: string;
    url: string;
    remainingLifeSeconds: number;
    localExpirationTime: number;
}