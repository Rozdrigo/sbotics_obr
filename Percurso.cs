void Main()
{
    int passosNoVazio = 0;
    int[] linha = new int[105];
    
    bool estaArena = false;
    bool clive = false;

    while (true)
    {
        // CURVAS 90°
        Func<int, int, int, int> encLinha = (direitaE, rotacoes, verde90) =>
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

        if (estaArena)
        {
            bc.MoveFrontalAngles(300, 90);

            if (!bc.Touch(1))
            {
                bc.MoveFrontalRotations(-300, 8);
            }
            bc.MoveFrontalRotations(300, 0.5f);
            bc.MoveFrontalAngles(300, -90);

            int cm = 0;
            while (true)
            {
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
                } // AJUSTE
                else if (bc.ReturnColor(1) == "PRETO" || bc.ReturnColor(0) == "PRETO")
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
                } // FORA DE LINHA [PARADO]
                else
                {
                    bc.MoveFrontalRotations(180, 1);
                    passosNoVazio++;
                    if (passosNoVazio > 14)
                    {
                        passosNoVazio = 0;
                        bc.MoveFrontalRotations(-180, 20);
                    }
                }
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
                encLinha(1, 15, 1);
            } //MARCAÇÕES VERDES ESQUERDA
            else if (bc.ReturnColor(3) == "VERDE" && bc.ReturnColor(1) == "BRANCO")
            {
                encLinha(-1, 15, 1);
            } // CURVA DE 90 LINHA PRETA DIREITA
            else if (bc.ReturnColor(0) == "PRETO" && bc.ReturnColor(1) == "PRETO" && bc.ReturnColor(3) == "BRANCO" && bc.ReturnColor(4) == "BRANCO")
            {
                encLinha(1, 14, 0);
            } // CURVA DE 90 LINHA PRETA ESQUERDA
            else if (bc.ReturnColor(0) == "BRANCO" && bc.ReturnColor(1) == "BRANCO" && bc.ReturnColor(3) == "PRETO" && bc.ReturnColor(4) == "PRETO")
            {
                encLinha(-1, 14, 0);
            }
            //ANDANDO SEM LINHA
            else if (bc.ReturnColor(0) == "BRANCO" && bc.ReturnColor(1) == "BRANCO" && bc.ReturnColor(2) == "BRANCO" && bc.ReturnColor(3) == "BRANCO" && bc.ReturnColor(4) == "BRANCO")
            {
                count++;
                bc.Print(count);
            }
            // SUBINDO A RAMPA
            if (bc.Inclination() > 330 && bc.DetectDistance(1, 30, 40))
            {
                clive = true;
            }
            // ANALIZANDO ESTADO ANTERIOR DO CARRINHO
            else if (clive && bc.Inclination() == 0)
            {
                estaArena = true;
            }
        };
        bc.Print(" ESTOU NA ARENA: " + estaArena + " PASSEI NA RAMPA: " + clive);
    }
}