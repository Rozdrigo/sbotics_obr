void Main()
{
    int passosNoVazio = 0;
    int[] linha = new int[102];

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
        Func <int, string> quatroCantos = (num) => {
            while(!(bc.Compass() < 135.5 && bc.Compass() > 134.5)){
                bc.MoveFrontalAngles(300, 0.1f);
            }
            while(!bc.DetectDistance(0, 183, 184)){
                bc.MoveFrontal(170, 170);
            }
            return "as";
        };
        if (estouArena)
        {
           quatroCantos(0);
        }
        else
        {
            //DESVIAR DE OBSTACULOS
            if (bc.DetectDistance(2, 15, 25) && bc.DetectDistance(0, 0, 35) || bc.DetectDistance(2, 15, 25) && !bc.DetectDistance(0, 0, 800))
            {
                float AngAntigo = bc.Compass();
                bc.MoveFrontalAngles(500, -45);
                if (!bc.DetectDistance(2, 15, 20))
                {
                    bc.ActuatorSpeed(150);
                    bc.ActuatorUp(1000);
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
                girarParaLinha(1, 15, 1);
            } //MARCAÇÕES VERDES ESQUERDA
            else if (bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) != "VERDE")
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