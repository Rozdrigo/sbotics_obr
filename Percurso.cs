void Main()
{
    bool estaArena = false;
    bool clive = false;
    while (true)
    {
        float[] sens ={
            bc.Lightness(0), bc.Lightness(1), bc.Lightness(2), bc.Lightness(3), bc.Lightness(4), bc.Lightness(5)
        };
        int count = 0;
        //AJUSTE DE CURVAS SUAVES
        Func<int> ajusteFino = () =>
        {
            float mediana = (sens[1] + sens[2] + 40) / 2;
            float error = 40 - mediana;
            bc.MoveFrontal(50 - 15 * error, 50 + 15 * error);

            return 0;
        };

        // CURVAS 90° 
        Func<int, int, int, int> curvasDe90 = (direitaE, rotacoes, verde90) =>
        {
            bc.MoveFrontalRotations(300, rotacoes);
            bc.Wait(500);
            bc.MoveFrontalAngles(500, 45 * verde90 * direitaE);
            while (bc.ReturnColor(2) != "PRETO")
            {
                bc.MoveFrontal(-1000 * direitaE, 1000 * direitaE);
            }
            return 0;
        };

        //PROXIMIDADE DE NUMEROS
        // Func<float, float> calcAngulos = (ang) => {
        //   float[]angRetos = {0, 90, 180, 270, 360};
        //   for (int i = 0; i < 5; i++){
        //     angRetos[i] = (ang - angRetos[i]);
        //   };
        //   float[]aux = {Math.Abs(angRetos[0]), Math.Abs(angRetos[1]), Math.Abs(angRetos[2]), Math.Abs(angRetos[3]), Math.Abs(angRetos[4])};
        //   return angRetos[aux.Min()];
        // };

        //LINHAS RETAS
        if (bc.ReturnColor(0) != "PRETO" && bc.ReturnColor(4) != "PRETO")
        {
            int contador = 0;
            //ANDAR PRA FRENTE
            if (bc.ReturnColor(2) == "PRETO" && !bc.DetectDistance(2, 20, 25))
            {
                bc.MoveFrontal(170, 170);
            } // AJUSTE 
            else if (bc.ReturnColor(1) == "PRETO")
            {  
                bc.MoveFrontal(-1000, 1000);
            } // AJUSTE
            else if (bc.ReturnColor(3) == "PRETO")
            {   
                bc.MoveFrontal(1000, -1000);
            } // FORA DE LINHA [PARADO]
            else
            {  
                bc.MoveFrontal(170, 170);
            }
        }   //DESVIAR DE OBSTACULOS
        else if (bc.DetectDistance(2, 20, 25))
        { //RAMPA
            float AngAntigo = bc.Compass();
            bc.MoveFrontalAngles(500, -45);
            if (!bc.DetectDistance(2, 15, 20))
            {
                bc.ActuatorSpeed(150);
                bc.ActuatorUp(1000);
                bc.MoveFrontalRotations(300, 18);
                bc.MoveFrontalAngles(300, 45);
                while (!bc.DetectDistance(1, 0, 30))
                {
                    bc.MoveFrontal(180, 180);
                };
                bc.MoveFrontalRotations(500, 18);
                bc.MoveFrontalAngles(500, 45);
                bc.MoveFrontalRotations(300, 18);
                bc.MoveFrontalAngles(500, -45);
            }
        } //MARCAÇÕES VERDES DIREITA
        else if (bc.ReturnColor(1) == "VERDE" && bc.ReturnColor(3) == "BRANCO")
        {
            curvasDe90(1, 15, 1);
        } //MARCAÇÕES VERDES ESQUERDA
        else if (bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) == "BRANCO")
        {
            curvasDe90(-1, 15, 1);
        } // CURVA DE 90 LINHA PRETA DIREITA
        else if (bc.ReturnColor(0) == "PRETO" && bc.ReturnColor(1) == "PRETO" && bc.ReturnColor(3) == "BRANCO" && bc.ReturnColor(4) == "BRANCO")
        {
            curvasDe90(1, 14, 0);
        } // CURVA DE 90 LINHA PRETA ESQUERDA
        else if (bc.ReturnColor(0) == "BRANCO" && bc.ReturnColor(1) == "BRANCO" && bc.ReturnColor(3) == "PRETO" && bc.ReturnColor(4) == "PRETO")
        {
            curvasDe90(-1, 14, 0);
        }
        //ANDANDO SEM LINHA
        else if(bc.ReturnColor(0) == "BRANCO" && bc.ReturnColor(1) == "BRANCO" && bc.ReturnColor(2) == "BRANCO" && bc.ReturnColor(3) == "BRANCO" && bc.ReturnColor(4) == "BRANCO"){
            count++;
            bc.Print(count);
        }
        // SUBINDO A RAMPA
        if(bc.Inclination() >  330 && bc.DetectDistance(1, 30, 40)){
            clive = true;
        }
        // ANALIZANDO ESTADO ANTERIOR DO CARRINHO
        else if (clive && bc.Inclination() == 0){
            estaArena = true;
        }
        bc.Print("II "+ estaArena + " sa " + clive);
        if(estaArena){
            bc.MoveFrontalAngles(300, 90);
            bc.MoveFrontalRotations(-300, 90);
            bc.MoveFrontalAngles(300, -90);
        };
    }
}