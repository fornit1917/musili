const TEMPO_KEY = "msl_tempo";
const GENRES_KEY = "msl_tempo"

export default class AppStorage {
    public setTempo(tempo: string) {
        localStorage.setItem(TEMPO_KEY, tempo);
    }

    public setGenres(genres: string[]) {
        localStorage.setItem(GENRES_KEY, genres.join(","));
    }

    public getTempo(): string {
        const tempo = localStorage.getItem(TEMPO_KEY);
        return tempo ? tempo : "any";
    }

    public getGenresAsString(): string {
        const genres = localStorage.getItem(GENRES_KEY);
        return genres ? genres : "classical,electronic";
    }

    public getGenres(): string[] {
        return this.getGenresAsString().split(",");
    }
}