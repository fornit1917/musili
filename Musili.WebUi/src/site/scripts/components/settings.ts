import { addEventListenerToList, nodeListToArray } from "../utils/dom-utils";
import AppStorage from "../services/app-storage";
import { TracksCriteria } from "../services/types";

const SELECTED_CLASS = "selectable-btn--selected";

export default class Settings {
    private root: HTMLElement;
    private tempos: HTMLElement[];
    private genres: HTMLElement[];
    private storage: AppStorage;
    private changesCallback: () => void;

    public isHidden: boolean = false;

    constructor(selector: string, storage: AppStorage) {
        this.storage = storage;
        this.root = document.querySelector(selector);
        const tempos = this.root.querySelectorAll(".js-tempo");
        const genres = this.root.querySelectorAll(".js-genre");
        addEventListenerToList(tempos, "click", e => { this.onTempoClick(e as MouseEvent); });
        addEventListenerToList(genres, "click", e => { this.onGenreClick(e as MouseEvent); });

        this.tempos = nodeListToArray(tempos);
        this.genres = nodeListToArray(genres);

        const storedSettings = this.getSettingsFromStore();
        this.tempos.forEach(btn => {
            if (storedSettings.tempo === btn.dataset.id) {
                btn.classList.add(SELECTED_CLASS);
                return;
            }
        });
        this.genres.forEach(btn => {
            if (storedSettings.genres.some(item => item === btn.dataset.id)) {
                btn.classList.add("selectable-btn--selected");
            }
        });
    }

    public setCallback(cb: () => void): void {
        this.changesCallback = cb;
    }

    public hide() {
        this.root.classList.add("tracks-settings-container--hidden");
        setTimeout(() =>{
            this.root.classList.remove("tracks-settings-container--initial");
            this.root.style.display = "none";
            this.isHidden = true;
        }, 300);
    }

    public show() {
        this.root.style.display = "block";
        setTimeout(() => {
            this.root.classList.remove("tracks-settings-container--hidden");
            this.isHidden = false;
        }, 100);
    }

    private notifyAboutChanges() {
        if (this.changesCallback) {
            this.changesCallback();
        }
    }

    private getSettingsFromStore(): TracksCriteria {
        return {
            tempo: this.storage.getTempo(),
            genres: this.storage.getGenres(),
        };
    }

    private getSelectedGenresButtons(): HTMLElement[] {
        return this.genres.filter(item => item.classList.contains(SELECTED_CLASS));
    }

    private getSelectedGenres(): string[] {
        return this.getSelectedGenresButtons().map(item => item.dataset.id);
    }

    private onTempoClick(e: MouseEvent) {
        this.tempos.forEach(btn => {
            if (btn.dataset.id === (e.target as HTMLElement).dataset.id) {
                btn.classList.add(SELECTED_CLASS);
                this.storage.setTempo(String(btn.dataset.id));
                this.notifyAboutChanges();
            } else {
                btn.classList.remove(SELECTED_CLASS);
            }
        });
    }

    private onGenreClick(e: MouseEvent) {
        const selectedGenres = this.getSelectedGenresButtons();
        const target = e.target as HTMLElement;
        if (selectedGenres.length === 1 && selectedGenres[0].dataset.id === target.dataset.id) {
            return;
        }
        target.classList.toggle(SELECTED_CLASS);
        this.storage.setGenres(this.getSelectedGenres());
        this.notifyAboutChanges();
    }
}