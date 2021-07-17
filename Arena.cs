void Main(){
    int cm = 0;
    while(true){
        // if(!bc.DetectDistance(0, 0, 1)){
        //     bc.MoveFrontalRotations(100, cm);
        //     cm++;
        // }
        // bc.Print(cm);
        bool arena = true;
        if(arena){
            bc.MoveFrontalAngles(300, 90);
            bc.MoveFrontalRotations(300, -20);
            bc.MoveFrontalAngles(300, -90);
        };
    };
}