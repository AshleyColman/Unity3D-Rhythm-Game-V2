<?php

// Database variables
$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

// Form variables from C# script
$username = $_POST["username"];
$beatmapID = $_POST["id"];
$error = "error";

// Connect to database
$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

// Check connection to database
if (mysqli_connect_errno())
{
    echo $error;
    exit();
}
else
{
        // Retrieve creator information
        $sql = "SELECT * FROM `players` WHERE username ='" . $username . "';";

        $sqlCheck = mysqli_query($con, $sql);
        
        if (mysqli_num_rows($sqlCheck) != 1)
        {
            echo $error;
            exit();
        }
        
        // Get information from table
        $infoarray = mysqli_fetch_assoc($sqlCheck);
    
        // Assign to array
        $returnarray[0] = $infoarray["image_url"];
        $returnarray[1] = $infoarray["level"];



        // Retrieve online information
        $sql = "SELECT * FROM `ranked_beatmaps` WHERE id ='" . $beatmapID . "';";

        $sqlCheck = mysqli_query($con, $sql);
        
        if (mysqli_num_rows($sqlCheck) != 1)
        {
            echo $error;
            exit();
        }
        
        // Get information from table
        $infoarray = mysqli_fetch_assoc($sqlCheck);
    
        // Assign to array
        $returnarray[2] = $infoarray["downloads"];
        $returnarray[3] = $infoarray["plays"];
        $returnarray[4] = $infoarray["favorites"];
        $returnarray[5] = $infoarray["clears"];
        $returnarray[6] = $infoarray["fails"];
        $returnarray[7] = $infoarray["ranked_date"];

        // Split array and send back to C# script
        die (implode("->", $returnarray));
}
?>
