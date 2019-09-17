export function addEventListenerToList(list: NodeList, event: string, handler: EventListener) {
    for (let i = 0; i < list.length; i++) {
        list[i].addEventListener("click", handler);
    }
}

export function nodeListToArray(list: NodeList): HTMLElement[] {
    const result = new Array<HTMLElement>(list.length);
    for (let i = 0; i < list.length; i++) {
        result[i] = list[i] as HTMLElement;
    }
    return result;
}