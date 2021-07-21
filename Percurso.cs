void Main()
{
    int passosNoVazio = 0;
    int[] linha = new int[105];
    
    bool estouArena = false;
    bool passRampa = false;

    while (true)
    {
        // CURVAS 90°
        Func<int, int, int, int> girarParaLinha = (direcao, rotacao, verde) =>
        {
            bc.MoveFrontalRotations(300, rotacao);
            bc.Wait(500);
            bc.MoveFrontalAngles(500, 45 * verde * direcao);
            while (bc.ReturnColor(2) != "PRETO")
            {
                bc.MoveFrontal(-1000 * direcao, 1000 * direcao);
            }
            return 0;
        };

        if (estouArena)
        {
            bc.MoveFrontalAngles(300, 90);

            if (!bc.Touch(1))
            {
                bc.MoveFrontalRotations(-300, 8);
            }
            int cm = 0;
            while (true)
            {
                if(bc.Compass() < 92 && bc.Compass() > 88){
                if(bc.Compass() > 90){
                    bc.MoveFrontalAngles(300, -0.01f);
                }else if(bc.Compass() < 90){
                    bc.MoveFrontalAngles(300, 0.01f);
                };
                };
                bc.MoveFrontalRotations(300, 0.2f);
                bc.MoveFrontalRotations(300, -90);
                if (!bc.DetectDistance(0, 0, 1))
                {
                    cm++;
                    bc.MoveFrontalRotations(100, 1);
                    if (bc.DetectDistance(1, 0, 240))
                    {
                        linha[cm - 1] = 1;
                    }
                    else
                    {
                        linha[cm - 1] = 0;
                    }
                }
                //bc.Print(cm);
                bc.Print(string.Join(",", linha));
            };
        }
        else
        {
            //LINHAS RETAS
            if (bc.ReturnColor(0) != "PRETO" && bc.ReturnColor(4) != "PRETO")
            {
                //ANDAR PRA FRENTE
                if (bc.ReturnColor(2) == "PRETO" && !bc.DetectDistance(2, 20, 25))
                {
                    bc.MoveFrontal(170, 170);
                }
                 // FORA DE LINHA [PARADO]
                else if ( bc.Lightness(0) > 40 && bc.Lightness(1) > 40 && bc.Lightness(2) > 40 && bc.Lightness(3) > 40 && bc.Lightness(4) > 40 && bc.Inclination() < 15)
                {
                    bc.MoveFrontalRotations(170, 1);
                    passosNoVazio++;
                    if (passosNoVazio > 20)
                    {
                        passosNoVazio = 0;
                        bc.Wait(200);
                        bc.MoveFrontalRotations(-170, 30);
                    }
                }
                else{
                    bc.MoveFrontal(170, 170);
                }
                //AJUSTE
                if (bc.ReturnColor(1) == "PRETO" || bc.ReturnColor(0) == "PRETO")
                { 
                    while (bc.ReturnColor(2) != "PRETO")
                    {
                        bc.MoveFrontal(-1000, 1000);
                    }
                } // AJUSTE
                else if (bc.ReturnColor(3) == "PRETO" || bc.ReturnColor(4) == "PRETO")
                {
                    while (bc.ReturnColor(2) != "PRETO")
                    {
                        bc.MoveFrontal(1000, -1000);
                    }
                };
            } //DESVIAR DE OBSTACULOS
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
                        bc.MoveFrontal(170, 170);
                    };
                    bc.MoveFrontalRotations(500, 18);
                    bc.MoveFrontalAngles(500, 45);
                    bc.MoveFrontalRotations(300, 18);
                    bc.MoveFrontalAngles(500, -45);
                }
            } //MARCAÇÕES VERDES DIREITA
            else if (bc.ReturnColor(1) == "VERDE" && bc.ReturnColor(3) == "BRANCO")
            {
                girarParaLinha(1, 15, 1);
            } //MARCAÇÕES VERDES ESQUERDA
            else if (bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) == "BRANCO")
            {
                girarParaLinha(-1, 15, 1);
            } // CURVA DE 90 LINHA PRETA DIREITA
            else if (bc.ReturnColor(0) == "PRETO" && bc.ReturnColor(1) == "PRETO" && bc.ReturnColor(3) == "BRANCO" && bc.ReturnColor(4) == "BRANCO")
            {
                girarParaLinha(1, 14, 0);
            } // CURVA DE 90 LINHA PRETA ESQUERDA
            else if (bc.ReturnColor(0) == "BRANCO" && bc.ReturnColor(1) == "BRANCO" && bc.ReturnColor(3) == "PRETO" && bc.ReturnColor(4) == "PRETO")
            {
                girarParaLinha(-1, 14, 0);
            };

            // SUBINDO A RAMPA
            if (bc.Inclination() > 330 && bc.DetectDistance(1, 30, 40))
            {
                passRampa = true;
            }
            // ANALIZANDO ESTADO ANTERIOR DO CARRINHO
            else if (passRampa && bc.Inclination() == 0)
            {
                estouArena = true;
            }
        };
        bc.Print(" ESTOU NA ARENA: " + estouArena + " PASSEI NA RAMPA: " + passRampa);
    }
}