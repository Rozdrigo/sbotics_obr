void Main()
{
    int passosNoVazio = 0;
    string[] cantos = {null, null, null, null};

    bool estouArena = false;
    bool passRampa = false;
    bc.ActuatorSpeed(150);
    bc.ActuatorUp(1000);
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
        Func <int, int> meiaVolta = (num) => {
            girarParaLinha(1, 0, 4);
            return 0;
        };
        if (estouArena)
        {
            bool leituraFeita = false;
            int cont = 0;
            bc.Print(string.Join(",", cantos));
            cantos[0] = "ENTRADA";
            bc.MoveFrontalAngles(300, 45);
            bc.MoveFrontalRotations(300, (float)(160/2.3333333333333335));
            if(cantos[0] != null && cantos[1] != null && cantos[2] != null && cantos[3] != null){
                leituraFeita = true;
            }
            while(!leituraFeita){
                bc.MoveFrontalAngles(300, 1);
                if(bc.Compass(0) - bc.Compass(2) > 40 && bc.Compass(0) - bc.Compass(2) < 80){
                    cantos[cont++];
                }
            };
        }
        else
        {
            //DESVIAR DE OBSTACULOS
            if (bc.DetectDistance(2, 15, 25) && bc.DetectDistance(0, 0, 37) && bc.Inclination() == 0|| bc.DetectDistance(2, 15, 25) && !bc.DetectDistance(0, 0, 800) && bc.Inclination() == 0)
            {
                float AngAntigo = bc.Compass();
                bc.MoveFrontalAngles(500, -45);
                if (!bc.DetectDistance(2, 15, 20))
                {
                    bc.MoveFrontalRotations(300, 16);
                    bc.MoveFrontalAngles(300, 45);
                    while (!bc.DetectDistance(1, 0, 30))
                    {
                        bc.MoveFrontal(170, 170);
                    };
                    bc.MoveFrontalRotations(500, 18);
                    bc.MoveFrontalAngles(500, 45);
                    bc.MoveFrontalRotations(300, 16);
                    while (bc.ReturnColor(2) != "PRETO")
                    {
                        bc.MoveFrontal(1000, -1000);
                    };
                }
            }
            //LINHAS RETAS
            else if (bc.ReturnColor(0) != "PRETO" && bc.ReturnColor(4) != "PRETO")
            {
                //ANDAR PRA FRENTE
                if (bc.ReturnColor(2) == "PRETO" && !bc.DetectDistance(2, 20, 25))
                {
                    bc.MoveFrontal(170, 170);
                }
                // FORA DE LINHA [PARADO]
                else if (bc.Lightness(0) > 40 && bc.Lightness(1) > 40 && bc.Lightness(2) > 40 && bc.Lightness(3) > 40 && bc.Lightness(4) > 40 && bc.Inclination() < 15)
                {
                    while (bc.ReturnColor(0) == "BRANCO" && bc.ReturnColor(1) == "BRANCO" && bc.ReturnColor(2) == "BRANCO" && bc.ReturnColor(3) == "BRANCO" && bc.ReturnColor(4) == "BRANCO" && !passRampa)
                    {
                        bc.MoveFrontalRotations(170, 1);
                        passosNoVazio++;
                        if (passosNoVazio > 20)
                        {
                            bc.Wait(200);
                            bc.MoveFrontalRotations(-170, passosNoVazio + 10);
                            passosNoVazio = 0;
                        }
                    }
                }
                else
                {
                    bc.MoveFrontalRotations(170, 1);
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
            }
            //MARCAÇÕES VERDES DIREITA
            if (bc.ReturnColor(1) == "VERDE" && bc.ReturnColor(3) != "VERDE")
            {
                bc.MoveFrontalRotations(300, 0.2f);
                if(bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) == "VERDE"){
                    meiaVolta(0);
                }else{
                girarParaLinha(1, 15, 1);
                }
            } //MARCAÇÕES VERDES ESQUERDA
            else if (bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) != "VERDE")
            {
                bc.MoveFrontalRotations(300, 0.2f);
                if(bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) == "VERDE"){
                    meiaVolta(0);
                }else{
                girarParaLinha(-1, 15, 1);
                }
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