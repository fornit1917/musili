export function formatSeconds(time: number) {
    const min = Math.floor(time / 60);
    const sec = Math.floor(time - 60*min);
    return sec < 10 ? `${min}:0${sec}` : `${min}:${sec}`;
}