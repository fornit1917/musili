import { addEventListenerToList, nodeListToArray } from "../utils/dom-utils";
import TracksSettingsStorage from "../services/tracks-settings-storage";
import { TracksCriteria } from "../dto-types";

const SELECTED_CLASS = "selectable-btn--selected";

export default class TracksSettings {
    private storage: TracksSettingsStorage;
    private root: HTMLElement;
    private temposButtons: HTMLElement[];
    private genresButtons: HTMLElement[];
    private onChanged: () => void;

    constructor(selector: string, onChanged: () => void) {
        this.storage = new TracksSettingsStorage();
        this.onChanged = onChanged;

        this.root = document.querySelector(selector);
        const temposButtons = this.root.querySelectorAll(".js-tempo");
        const genresButtons = this.root.querySelectorAll(".js-genre");
        addEventListenerToList(temposButtons, "click", e => { this.onTempoClick(e as MouseEvent); });
        addEventListenerToList(genresButtons, "click", e => { this.onGenreClick(e as MouseEvent); });
        this.temposButtons = nodeListToArray(temposButtons);
        this.genresButtons = nodeListToArray(genresButtons);

        const storedSettings = this.getCurrentSettings();
        this.temposButtons.forEach(btn => {
            if (storedSettings.tempo === btn.dataset.id) {
                btn.classList.add(SELECTED_CLASS);
                return;
            }
        });
        if (this.temposButtons.every(btn => !btn.classList.contains(SELECTED_CLASS))) {
            this.storage.setTempo("any");
            this.temposButtons.find(btn => btn.dataset.id === "any").classList.add(SELECTED_CLASS);
        }

        this.genresButtons.forEach(btn => {
            if (storedSettings.genres.some(item => item === btn.dataset.id)) {
                btn.classList.add(SELECTED_CLASS);
            }
        });
        if (this.genresButtons.every(btn => !btn.classList.contains(SELECTED_CLASS))) {
            this.storage.setGenres(['classical','electronic']);
            this.genresButtons
                .filter(btn => btn.dataset.id === 'classical' || btn.dataset.id === 'electronic')
                .forEach(btn => btn.classList.add(SELECTED_CLASS));
        }
    }

    public hide(): Promise<void> {
        return new Promise(resolve => {
            this.root.classList.add("tracks-settings-container--hidden");
            setTimeout(() =>{
                this.root.classList.remove("tracks-settings-container--initial");
                this.root.style.display = "none";
                resolve();
            }, 100);
        });
    }

    public show(): Promise<void> {
        return new Promise(resolve => {
            this.root.style.display = "block";
            setTimeout(() => {
                this.root.classList.remove("tracks-settings-container--hidden");
                resolve();
            }, 100);
        })
        
    }

    public getCurrentSettings(): TracksCriteria {
        return {
            tempo: this.storage.getTempo(),
            genres: this.storage.getGenres(),
        };
    }

    private getSelectedGenresButtons(): HTMLElement[] {
        return this.genresButtons.filter(item => item.classList.contains(SELECTED_CLASS));
    }

    private getSelectedGenres(): string[] {
        return this.getSelectedGenresButtons().map(item => item.dataset.id);
    }

    private onTempoClick(e: MouseEvent) {
        this.temposButtons.forEach(btn => {
            if (btn.dataset.id === (e.target as HTMLElement).dataset.id) {
                btn.classList.add(SELECTED_CLASS);
                this.storage.setTempo(String(btn.dataset.id));
                this.onChanged();
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
        this.onChanged();
    }
}