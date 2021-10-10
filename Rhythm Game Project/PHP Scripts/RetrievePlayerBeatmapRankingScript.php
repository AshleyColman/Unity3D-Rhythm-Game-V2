<?php

// Database variables.
$dbServername = "localhost";
$dbUsername = "root";
$dbPassword = "";
$dbName = "rhythmgamex";

// Connect to database.
$con = mysqli_connect($dbServername, $dbUsername, $dbPassword, $dbName);

// Form variables from C# script.
$databaseTable = $_POST["databaseTable"];
$index = $_POST["index"];
$categorySorting = $_POST["categorySorting"];
$error = "error";

// Check connection to database.
if (mysqli_connect_errno())
{
    echo $error;
    exit();
}
else
{
    // Get the sorting based on the value passed from C# beatmap ranking script.
    switch ($categorySorting)
    {
        case 0:
            $categorySorting = "score";
        break;
        case 1:
            $categorySorting = "accuracy";
        break;
        case 2:
            $categorySorting = "combo";
        break;
        default:
        $categorySorting = "score";
        break;
    }

    // Retrieve player beatmap information.
    $sql = "SELECT * FROM `$databaseTable` ORDER BY `$categorySorting` DESC, `date` ASC LIMIT 1 OFFSET $index";
    $sqlCheck = mysqli_query($con, $sql);
    if (mysqli_num_rows($sqlCheck) != 1)
    {
        echo $error;
        exit();
    }
    
    // Get information from table.
    $infoarray = mysqli_fetch_assoc($sqlCheck);
    // Assign to return array
    $returnarray[0] = $infoarray["username"];
    $returnarray[1] = $infoarray["score"];
    $returnarray[2] = $infoarray["accuracy"];
    $returnarray[3] = $infoarray["combo"];
    $returnarray[4] = $infoarray["date"];
    $returnarray[5] = $infoarray["clear_points"];
    $returnarray[6] = $infoarray["hidden_points"];
    $returnarray[7] = $infoarray["mine_points"];
    $returnarray[8] = $infoarray["low_approach_rate_points"];
    $returnarray[9] = $infoarray["high_approach_rate_points"];
    $returnarray[10] = $infoarray["full_combo_points"];
    $returnarray[11] = $infoarray["max_percentage_points"];

    // Assign variables for future queries.
    $username = $returnarray[0];

    // Get player image url from table.
    $sql = "SELECT * FROM `players` WHERE username ='" . $username . "';";
    $sqlCheck = mysqli_query($con, $sql);
    if (mysqli_num_rows($sqlCheck) != 1)
    {
        echo $error;
        exit();
    }
    $playerinfoarray = mysqli_fetch_assoc($sqlCheck);
    $returnarray[12] = $playerinfoarray["image_url"];
    $returnarray[13] = $playerinfoarray["level"];
    
    // Return back to C# script.
    die (implode("->", $returnarray));
}
?>