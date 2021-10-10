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
$username = $_POST["username"];
$error = "error";

// Check the connect happened.
if (mysqli_connect_errno())
{
    echo $error;
    exit();
}
else
{
    // Retrieve all play information from beatmap leaderboard.
    $sql = "SELECT * FROM `$databaseTable` WHERE username ='" . $username . "';";

    $sqlCheck = mysqli_query($con, $sql);
    
    if (mysqli_num_rows($sqlCheck) != 1)
    {
        echo $error;
        exit();
    }
    
    // Get information from table.
    $infoarray = mysqli_fetch_assoc($sqlCheck);

    // Assign to array.
    $returnarray[0] = $infoarray["score"];
    $returnarray[1] = $infoarray["accuracy"];
    $returnarray[2] = $infoarray["combo"];
    $returnarray[3] = $infoarray["perfect"];
    $returnarray[4] = $infoarray["great"];
    $returnarray[5] = $infoarray["okay"];
    $returnarray[6] = $infoarray["miss"];
    $returnarray[7] = $infoarray["fever"];
    $returnarray[8] = $infoarray["bonus"];
    $returnarray[9] = $infoarray["date"];
    $returnarray[10] = $infoarray["clear_points"];
    $returnarray[11] = $infoarray["hidden_points"];
    $returnarray[12] = $infoarray["mine_points"];
    $returnarray[13] = $infoarray["low_approach_rate_points"];
    $returnarray[14] = $infoarray["high_approach_rate_points"];
    $returnarray[15] = $infoarray["full_combo_points"];
    $returnarray[16] = $infoarray["max_percentage_points"];

    // Assign variables for future queries.
    $score = $returnarray[0];
    $date = $returnarray[9];
    
    // RETRIEVE OVERALL PLACEMENT NUMBER #.
    $sql = "SELECT * FROM `$databaseTable` WHERE `score` > $score OR `score` = $score AND `date` < '$date'";
    $sqlCheck = mysqli_query($con, $sql);
    if (mysqli_num_rows($sqlCheck) == 0)
    {
        // Rank 1#.
        $placement = 1;
        $returnarray[17] = $placement;
    }
    else if (mysqli_num_rows($sqlCheck) > 0)
    {
        # Rank = total placements above user score and date.
        $placement = mysqli_num_rows($sqlCheck);
        $returnarray[17] = ($placement + 1);
    }

    
    // RETRIEVE TOTAL RECORD COUNT IN TABLE.
    $sql = "SELECT COUNT(*) FROM `$databaseTable`";
    $sqlCheck = mysqli_query($con, $sql);
    if (mysqli_num_rows($sqlCheck) != 1)
    {
        echo $error;
        exit();
    }
    $infoarray = mysqli_fetch_row($sqlCheck);
    $returnarray[18] = $infoarray[0];

    // Split array and send back to C# script.
    die (implode("->", $returnarray));
}
?>