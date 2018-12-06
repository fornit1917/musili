import Player from "./components/player";
import TracksSettings from "./components/tracks-settings";
import StartButton from "./components/start-button";
import Background from "./components/background";
import ApiClient from "./services/api-client";

class App {
    private startBtn: StartButton;
    private background: Background;
    private tracksSettings: TracksSettings;
    private player: Player;

    private isStarted: boolean = false;

    public constructor() {
        this.startBtn = new StartButton("#start-btn", () => { this.onAppStart(); });
        this.background = new Background("#bg");
        this.tracksSettings = new TracksSettings("#settings", () => { this.onTracksSettingsChanged(); });
        this.player = new Player("#player", new ApiClient(), {
            onHideSettings: () => this.onHideTracksSettings(),
            onShowSettings: () => this.onShowTracksSettings(),
        });
    }

    private onAppStart() {
        this.isStarted = true;
        this.background.show();
        this.tracksSettings.hide();
        this.player.loadAndStart(this.tracksSettings.getCurrentSettings());
    }

    private onShowTracksSettings() {
        return this.tracksSettings.show();
    }

    private onHideTracksSettings() {
        return this.tracksSettings.hide();
    }

    private onTracksSettingsChanged() {
        if (this.isStarted) {
            const settings = this.tracksSettings.getCurrentSettings();
            this.player.applyTracksSettingsAfterTimeout(settings);
        }
    }
}

(window as any).initMusiliApp = () => {
    const app = new App();
}