$(() => {
    let cat = new Example("Mruczek", 6);

    cat.getInfo();
});

// przykładowe użycie TypeScript 
// do nauki - potem do wyjebki
// pełna dokumentacja -> https://www.tutorialspoint.com/typescript/
class Example {
    catName: string;
    catAge: number;

    constructor(
        catName: string,
        catAge: number) {
        this.catName = catName;
        this.catAge = catAge;
    }

    getInfo(): void {
        let fullInfo = `Imię: ${this.catName}, Wiek: ${this.catAge}`;
        alert(fullInfo);
    }
}