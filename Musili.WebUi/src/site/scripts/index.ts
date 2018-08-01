import Player from "./components/player";
import Settings from "./components/settings";

(window as any).initMusiliApp = () => {
    const player = new Player();
    player.init("#player");

    const settings = new Settings();
    settings.init("#settings");

    console.log("App init!");
}