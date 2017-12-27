$(() => {
    let cat = new Example("Mruczek", 6);
    cat.getInfo();
});
// przykładowe użycie TypeScript 
// do nauki - potem do wyjebki
// pełna dokumentacja -> https://www.tutorialspoint.com/typescript/
class Example {
    constructor(catName, catAge) {
        this.catName = catName;
        this.catAge = catAge;
    }
    getInfo() {
        let fullInfo = `Imię: ${this.catName}, Wiek: ${this.catAge}`;
        alert(fullInfo);
    }
}
//# sourceMappingURL=main.js.map