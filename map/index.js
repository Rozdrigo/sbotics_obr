document.body.onload = adcElemento;

var array = [0,76,0,74,74,73,74,74,74,74,74,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,252,0,252,0,252,252,252,252,252,252,252,252,252,252,144,252,144,144,144,144,146,144,252,148,252,252,252,252,252,252,252,252,140,252,123,124,51,51,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
function adcElemento () {
    console.log(array.length, Math.max([array]))
  array.reverse().map((a) => {
    var divNova = document.createElement("div");
    divNova.style.height = "1px";
    divNova.style.width = a == 0? "300px": `${a/2.3333333333333335}px`;
    divNova.style.background = "red";
  
    var divAtual = document.getElementById("div1");
    document.body.insertBefore(divNova, divAtual);
  });
}