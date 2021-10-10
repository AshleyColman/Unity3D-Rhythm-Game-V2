<?php

// Database variables.
$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

// Connect to database.
$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

// Form variables from C# script.
$username = $_POST["username"];
$error = "error";

// Check connection to database.
if (mysqli_connect_errno())
{
    echo $error;
    exit();
}
else
{
    // Get player image url from table.
    $sql = "SELECT * FROM `players` WHERE username ='" . $username . "';";
    $sqlCheck = mysqli_query($con, $sql);
    if (mysqli_num_rows($sqlCheck) != 1)
    {
        echo $error;
        exit();
    }
    $playerinfoarray = mysqli_fetch_assoc($sqlCheck);
    $returnarray[0] = $playerinfoarray["image_url"];
    
    // Return back to C# script.
    die (implode("->", $returnarray));
}
?>