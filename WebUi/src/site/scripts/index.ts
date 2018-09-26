import Player from "./components/player";
import TracksSettings from "./components/tracks-settings";
import StartButton from "./components/start-button";
import Background from "./components/background";

class App {
    private startBtn: StartButton;
    private background: Background;
    private tracksSettings: TracksSettings;
    private player: Player;

    public constructor() {
        this.startBtn = new StartButton("#start-btn", () => { this.onAppStart(); });
        this.background = new Background("#bg");
        this.tracksSettings = new TracksSettings("#settings", () => { this.onTracksSettingsChanged(); });
        this.player = new Player("#player", {
            onHideSettings: () => { this.onHideTracksSettings(); },
            onShowSettings: () => { this.onShowTracksSettings(); },
        });
    }

    private onAppStart() {
        this.background.show();
        this.tracksSettings.hide();
        this.player.loadAndStart();
    }

    private onShowTracksSettings() {
        this.tracksSettings.show();
    }

    private onHideTracksSettings() {
        this.tracksSettings.hide();
    }

    private onTracksSettingsChanged() {
        const settings = this.tracksSettings.getCurrentSettings();
        this.player.applyTracksSettingsAfterTimeout(settings);
    }
}

(window as any).initMusiliApp = () => {
    const app = new App();
}