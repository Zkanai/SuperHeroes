

//manage a hero card to toggle the body with click on the hero name
function CardTitleToggleButton(cardTitle, cardBody) {
    let clickCount = 0;
    $(cardTitle).click(function () {
        $(cardBody).toggle(clickCount++ % 2 === 0);
    });
}

function CardImgSwapWithBody(cardTitle, cardImg) {
    $(cardTitle).click(function () {
        if ($(cardImg).css("display") == "none") {
            $(cardImg).css("display", "block");
        }
        else {
            $(cardImg).css("display", "none");
        }

    });
}

//manage if the picturec can't load
function PictureSwap() {
    $(".card .card-img").on("error", function () {
        $(this).prop("src", "../img/pictureNA.jpg");
    });
}

function BattleView() {

    CardTitleToggleButton("#userHeroCardTitle", "#userHeroCardBody");
    CardTitleToggleButton("#opponentHeroCardTitle", "#opponentHeroCardBody");

    //change between the hero img and stats
    CardImgSwapWithBody("#userHeroCardTitle", "#userHeroCardImg");
    CardImgSwapWithBody("#opponentHeroCardTitle", "#opponentHeroCardImg");

    //enable the fight button and flashing, 
    //highlight the chosen stat with red
    //disable the other inputs
    //trigger the enemy card show attributes
    //highlight in red the enemy corresponding attribute
    //when user select a stat on the battle view
    $(".card-body input").change(function () {
        $("input[name = battle]:checked").parent().css("color", "red");
        if ($(".form-check-input").is(":checked")) {
            $(".opponentHeroCardText").prop("hidden", false);
            $("#fight").css("pointer-events", "");
            $("#fight").prop("disabled", false);
            $(".noSkillChoosed h4").text("");
            $(".card-body input").prop("disabled", true);
            $("#opponentHeroCardTitle").trigger("click");
            let userHeroSkillName = $("input[name = battle]:checked").parent().children(".userHeroSkill").text();
            $("[id = '" + userHeroSkillName + "']").parent().css("color", "red");
        }
        $("input[name = battle]:not(:checked)").each(function () {
            $(this).parent().toggleClass("noHoverOnLabel");
        });

        setInterval(function () {
            $(".fightButton").toggleClass("fightRed");
        }, 500);
    });


    //here we post the the 2 heroes stats back to server
    //based on this stats the battle will start
    //we get back the result from the server
    $("#fight").click(function () {

        let userHeroName = $("#userHeroCardTitle span").text();
        let opponentHeroName = $("#opponentHeroCardTitle span").text();
        let userHeroSkillValue = $("input[name = battle]:checked").val();
        let userHeroSkillName = $("input[name = battle]:checked").parent().children(".userHeroSkill").text();
        let userHeroId = $("#UserHero_ApiId").val();
        let opponentHeroId = $("#OpponentHero_ApiId").val();
        let userHeroHp = +$("#dur").val();
        let userHeroDmg = +$("#pow").val();
        let userHeroSumStat = +$("#int").val() + +$("#str").val() + +$("#spd").val() + +$("#dur").val() + +$("#pow").val() + +$("#com").val();
        let opponentHeroHp = +$("#Durability").text();
        let opponentHeroDmg = +$("#Power").text();
        let opponentHeroSumStat = +$("#Intelligence").text() + +$("#Strength").text() + +$("#Speed").text() + +$("#Durability").text() + +$("#Power").text() + +$("#Combat").text();
        let opponentHeroSkillValue = 0;
        opponentHeroSkillValue = $("[id = '" + userHeroSkillName + "']").text();
        $("#battleSound")[0].play();

        if (userHeroId == null || opponentHeroId == null) {
            alert('error');
        }
        else {

            //post the data from the page
            //to the controller
            //based on data the battle procces start
            //on success got the battle result
            //inform the user in modal
            $.ajax({
                url: '/Battle/Battle',
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    leftHeroId: userHeroId, rightHeroId: opponentHeroId,
                    UserHeroName:userHeroName, UserHeroSkill:userHeroSkillValue, UserHeroHp:userHeroHp, UserHeroDmg:userHeroDmg, UserHeroStat:userHeroSumStat,
                    OpponentHeroName:opponentHeroName, OpponentHeroSkill:opponentHeroSkillValue, OpponentHeroHp:opponentHeroHp, OpponentHeroDmg:opponentHeroDmg, OpponentHeroStat:opponentHeroSumStat
                }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    //itt kell majd az a logika hogy ha még van a hp-ja a hősőknek, akkor újra lehessen skillt választani,
                    //és menjen még egy kört a csata
                    $("#result").text(data);
                    $("#resultModal").modal("show");

                },
                error: function (xhr) {
                    alert('Data reading error! Try reload the page!'); 
                }
            })
        }


    });

}

function ChooseHeroView() {

    CardTitleToggleButton("#chooseUserHeroCardTitle", "#chooseUserHeroCardBody");
    CardTitleToggleButton("#chooseOpponentHeroCardTitle", "#chooseOpponentHeroCardBody");

    CardImgSwapWithBody("#chooseUserHeroCardTitle", "#chooseUserHeroImg");
    CardImgSwapWithBody("#chooseOpponentHeroCardTitle", "#chooseOpponentHeroImg");


    //choose user hero
    $(".chooseUserHero").click(function () {
        if ($("#UserHeroApiIdList").val() != "" && $("#OpponentHeroApiIdList").val() != "") {
            $("#startButton").prop("disabled", false);
            $("#startButton").css("color", "red");
            $("#chooseHeroStart h4").first().text("");
        };

        if ($("#UserHeroApiIdList").val() != "" && $("#OpponentHeroApiIdList option:selected").text() == "Choose Hero") {
            $("#randomEnemyButton").prop("disabled", false);
            $("#randomEnemyButton").css("color", "red");
        };

        if ($("#UserHeroApiIdList").val() == "") {
            $("#randomEnemyButton").prop("disabled", true);
            $("#randomEnemyButton").css("color", "white");

        };

        if ($("#UserHeroApiIdList").val() == "" || $("#OpponentHeroApiIdList").val() == "") {
            $("#startButton").prop("disabled", true);
            $("#startButton").css("color", "white");
            $("#chooseHeroStart h4").first().text("CHOOSE YOUR HERO AND AN ENEMY HERO");
        }

        if ($("#UserHeroApiIdList option:selected").text() == "Choose Hero") {
            $("#userHeroCard").css("visibility", "hidden");
        }

        if ($("#UserHeroApiIdList").val() != "") {

            var userHeroApiId = 0;
            userHeroApiId = $("#UserHeroApiIdList").val();

            $.ajax({
                url: '/ChooseHeroView/ChosenUserHero',
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    UserHeroId: userHeroApiId,
                    UserHeroName: "", UserHeroRealName: "", UserHeroIntelligence: 0, UserHeroStrength: 0,
                    UserHeroSpeed: 0, UserHeroDurability: 0, UserHeroPower: 0, UserHeroCombat: 0,
                    UserHeroImgUrl: ""
                }),
                async: true,
                processData: false,
                cache: false,
                success: function (userHero) {

                    if (userHero.UserHeroId != 0) {
                        $("#userHeroCard img").prop("src", userHero.UserHeroImgUrl);
                        $("#userHeroCard h4 span").text(userHero.UserHeroName);
                        $("#userHeroRealName").text(userHero.UserHeroRealName);
                        $("#userHeroInt").text(userHero.UserHeroIntelligence);
                        $("#userHeroStr").text(userHero.UserHeroStrength);
                        $("#userHeroSpe").text(userHero.UserHeroSpeed);
                        $("#userHeroDur").text(userHero.UserHeroDurability);
                        $("#userHeroPow").text(userHero.UserHeroPower);
                        $("#userHeroCom").text(userHero.UserHeroCombat);
                        $("#userHeroCard").css("visibility", "visible");

                    }

                },
                error: function (xhr) {
                    alert('error');
                }
            });
        };

    });

    //choose opponenthero 
    $(".chooseOpponentHero").click(function () {
        if ($("#UserHeroApiIdList").val() != "" && $("#OpponentHeroApiIdList").val() != "") {
            $("#startButton").prop("disabled", false);
            $("#startButton").css("color", "red");
            $("#chooseHeroStart h4").first().text("");
        };

        if ($("#UserHeroApiIdList").val() == "" || $("#OpponentHeroApiIdList").val() == "") {
            $("#startButton").prop("disabled", true);
            $("#startButton").css("color", "white");
            $("#chooseHeroStart h4").first().text("CHOOSE YOUR HERO AND AN ENEMY HERO");
        }

        if ($("#OpponentHeroApiIdList option:selected").text() == "Choose Hero") {
            $("#opponentHeroCard").css("visibility", "hidden");
            $("#randomEnemyButton").prop("disabled", false);
            $("#randomEnemyButton").css("color", "red");

        }

        if ($("#OpponentHeroApiIdList").val() != "") {

            var opponentHeroApiId = 0;
            opponentHeroApiId = $("#OpponentHeroApiIdList").val();
            $("#randomEnemyButton").prop("disabled", true);
            $("#randomEnemyButton").css("color", "white");

            $.ajax({
                url: '/ChooseHeroView/ChosenOpponentHero',
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    OpponentHeroId: opponentHeroApiId,
                    OpponentHeroName: "", OpponentHeroRealName: "", OpponentHeroImgUrl: ""
                }),
                async: true,
                processData: false,
                cache: false,
                success: function (opponentHero) {

                    if (opponentHero.OpponentHeroId != 0) {
                        $("#opponentHeroCard img").prop("src", opponentHero.OpponentHeroImgUrl);
                        $("#opponentHeroCard h4 span").text(opponentHero.OpponentHeroName);
                        $("#opponentHeroRealName").text(opponentHero.OpponentHeroRealName);
                        $("#opponentHeroCard").css("visibility", "visible");
                    }


                },
                error: function (xhr) {
                    alert('error');
                }
            });
        };

    });


}

function SearchView() {
    let detailHeroId = "";
    $(".detailsButton").click(function () {
        detailHeroId = $(this).val();
        $("#detailsModal").modal("show");
    });

    //post the sought hero id to controller
    //get back the login url with hero id as parameter
    $(".heroToShow").click(function () {
        $.ajax({
            url: '/SearchView/HeroToLogin',
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                HeroId:detailHeroId
            }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {               
                
            },
            error: function (xhr) {
               
            }
        })
    });

}

$(document).ready(function () {

    PictureSwap();

    SearchView();

    ChooseHeroView();

    BattleView();

});