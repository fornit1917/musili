export default class Bg {
    private root: HTMLElement;

    constructor(selector: string) {
        this.root = document.querySelector(selector);
    }

    public show() {
        this.root.style.display = "block";
        setTimeout(() => {
            this.root.classList.remove("bg--hidden");
        }, 100);
    }
}