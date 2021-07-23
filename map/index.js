document.body.onload = adcElemento;

var array = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,22,21,19,19,19,19,20,20,19,20,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,247,118,247,115,116,115,115,115,115,117,115,246,247,36,36,36,36,36,36,36,36,40,41,97,97,97,97,209,98,205,212,201,207,196,202,191,196,187,192,181,186,176,181]
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