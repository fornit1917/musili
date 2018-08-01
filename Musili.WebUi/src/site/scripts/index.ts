import Player from "./components/player";
import Settings from "./components/settings";
import StartButton from "./components/start-button";

(window as any).initMusiliApp = () => {
    const settings = new Settings("#settings");
    const player = new Player("#player", settings);
    const startBtn = new StartButton("#start-btn", player, settings);
}