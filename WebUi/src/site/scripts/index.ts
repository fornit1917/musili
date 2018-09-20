import Player from "./components/player";
import Settings from "./components/settings";
import StartButton from "./components/start-button";
import AppStorage from "./services/app-storage";
import Bg from "./components/bg";

(window as any).initMusiliApp = () => {
    const storage = new AppStorage();
    const settings = new Settings("#settings", storage);
    const bg = new Bg("#bg");
    const player = new Player("#player", storage, settings);
    const startBtn = new StartButton("#start-btn", player, settings, bg);
}