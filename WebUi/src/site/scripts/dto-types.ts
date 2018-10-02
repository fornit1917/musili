export interface TracksCriteria {
    tempo: string;
    genres: string[];
}

export interface Track {
    artist: string;
    title: string;
    url: string;
    remainingLifeSeconds: number;
    localExpirationTime: number;
}