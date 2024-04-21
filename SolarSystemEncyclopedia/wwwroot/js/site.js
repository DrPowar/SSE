const pageHeight = Math.max(document.body.scrollHeight, document.body.offsetHeight,
    document.documentElement.clientHeight, document.documentElement.scrollHeight, document.documentElement.offsetHeight);

const bottomValue = pageHeight + 5500.00;

function multipleBoxShadow(n) {
    let value = '';
    for (let i = 0; i < n; i++) {
        value += `${getRandomInt(bottomValue)}px ${getRandomInt(bottomValue)}px #FFF`;
        if (i !== n - 1) {
            value += ', ';
        }
    }
    return value;
}

function getRandomInt(max) {
    return Math.floor(Math.random() * Math.floor(max));
}

const shadowsSmall = multipleBoxShadow(2000);
const shadowsMedium = multipleBoxShadow(600);
const shadowsBig = multipleBoxShadow(400);

document.documentElement.style.setProperty('--shadows-small', shadowsSmall);
document.documentElement.style.setProperty('--shadows-medium', shadowsMedium);
document.documentElement.style.setProperty('--shadows-big', shadowsBig);
