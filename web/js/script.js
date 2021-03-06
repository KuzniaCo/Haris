jQuery(document).ready(function($) {
	var fixHelper = function(e, ui) {
		ui.children().each(function() {
			$(this).width($(this).width());
		});
		return ui;
	};

	$("#main_table tbody").sortable({
		helper: fixHelper,
		items: '> tr:not(:first)',
		appendTo: "parent"
	}).disableSelection();
	
	var encja = null;
	var condition = '';
	$("body").delegate("a", "click", function(e) {
		if($(this).attr("data-show") != null) {
			var element = $(this).attr("data-show");
			if($(element).is(":visible"))
				$(element).removeClass("active").fadeOut();
			else
				$(element).addClass("active").fadeIn();
		}
		else if($(this).attr("data-typ") != null && $(this).attr("data-typ") == "add-element") {
			encja = $(this);
			$(".overlay").fadeIn();
			$(".if").addClass("active").fadeIn();
			
			$(".if").find(".active").removeClass("active").hide();
			$(".if").find(".guid-" + $(this).attr("data-guid")).addClass("active").show();
			
			$(".elements").removeClass("active").hide();
		}
		else if($(this).attr("data-typ") != null && $(this).attr("data-typ") == "add-variable") {
			$(".overlay").fadeIn();
			$(".variable").addClass("active").fadeIn();
		}
		else if($(this).attr("data-if") != null) {
			if($(this).attr("data-if") == "if" || $(this).attr("data-if") == "elseif") {
				$(".if").removeClass("active").hide();
				$(".variable-condition").addClass("active").show();
				condition = $(this).attr("data-if");
			}
			else {
				var data = "\
				<tr>\
					<td>" + ($(this).text() != "else" ? $(encja).text() : "") + "</td>\
					<td>" + $(this).text() + "</td>\
					<td><div class='actiones'></div><div class='clear'></div><a add-action='' style='font-size: 13px; color: #afafaf; '>Add action</a></td>\
				</tr>";
				$("#main_table tbody").append(data).sortable('refresh');
				$(".if").removeClass("active").fadeOut();
				$(".overlay").fadeOut();
				encja = null;
			}
		}
		else if($(this).attr("add-action") != null) {
			encja = $(this).parent("td").parent("tr");
			$(".overlay").fadeIn();
			$(".actions").addClass("active").fadeIn();
		}
		else if($(this).attr("data-typ") != null && $(this).attr("data-typ") == "add-action") {
			$(".action-element .active").removeClass("active");
			$(this).addClass("active");
			
			$(".action2 .hidden").hide();
			if($(this).attr("data-type") == "variable") {
				$(".action2").find(".variableact2").addClass("active").show();
			}
			else {
				$(".action2").find(".guid-" + $(this).attr("data-guid")).addClass("active").show();
			}
		}
		else if($(this).attr("data-variable") != null) {
			$(".variableact2 .active").removeClass("active");
			$(this).addClass("active");
			$(".action3").addClass("active").show();
		}
		else if($(this).attr("data-action") != null) {
			var data = "<div>\
				<div class='tbl-1' style='border-right: 1px solid #afafaf'>\
					" + $(".action-element a.active").text() + "\
				</div>\
				<div class='tbl-2'>\
					" + $(this).text() + "\
				</div><div class='clear'></div></div>";
			$(encja).children().children(".actiones").append(data);

			$(".overlay").fadeOut();
			$(".actions").removeClass("active").hide();
			$(".action2 .hidden").hide();
			$(".action-element a.active").removeClass("active");
			encja = null;			
		}
		else if($(this).attr("close-div") != null) {
			var element = $(".active");
			if($(element).is(":visible")) {
				$(element).fadeOut();
				$(".overlay").fadeOut();
				encja = null;
			}
		}
		else if($(this).attr("data-generate") != null) {
			var variable = new Array();
			$("#variable_table tbody tr:not(.header)").each(function(i,v) {
				if(!$.trim($(v).html())) return;
				
				var nam = $.trim($(v).children("td:first").html());
				var def = $.trim($(v).children("td:last").html());
				variable.push({name: nam, default: def});
			});
			
			var action = new Array();
			$("#main_table tbody tr:not(.header)").each(function(i,v) {
				if(!$.trim($(v).html())) return;
				var nam = $.trim($(v).children("td:first").html());
				var conditionx = $.trim($(v).children("td:eq( 1 )").html());
				console.log($(this).find(".actiones div:not([class])"));
				var actionxs = Array();
				$(this).find(".actiones div:not([class])").each(function(x,y) {
					var gid = $(this).children("div:first").attr("data-guid") != null ? $.trim($(this).children("div:first").attr("data-guid")) : 0;
					var act = $.trim($(this).children("div.tbl-2").html());
					var variablex;
					if(!gid)
						variablex = $.trim($(this).find("div:first b").html());
					actionxs.push({guid: gid, variable: variablex, action: act});
				});
				action.push({encja: nam, condition: conditionx, actions: actionxs})
			});
			var output = {variables: variable, actions: action};
			
			$("#generated").html(JSON.stringify(output)).fadeIn();
		}
		e.stopPropagation();
	});
	$(document).on('submit','form[name=add_variable]',function() {
		var name = $(this).children("[name=variable_name]").val();
		var def = $(this).children("[name=variable_value]").val();
		$(this).children("[name=variable_value]").val('');
		$(this).children("[name=variable_name]").val('');
		var data = "<tr>\
					<td>\
						" + name + "\
					</td>\
					<td>\
						" + def + "\
					</td>\
				</tr>";
		$("#variable_table").append(data).show();
		
		data = "<option value=" + name + ">" + name + "</option>";
		$("form[name=condition_variable] select[name=variable_variable]").append(data);
		
		$(".overlay").fadeOut();
		$(".variable").removeClass("active").hide();
		return false;
	});
	
	$(document).on('submit','form[name=condition_variable]',function() {
		var variable = $(this).children("[name=variable_variable]").val();
		var type = $(this).children("[name=variable_condition]").val();
		var value = $(this).children("[name=variable_value]").val();
		$(this).children("[name=variable_value]").val('');
		
		var data = "\
				<tr class='can-sort'>\
					<td></td>\
					<td>" + condition + " <b>" + variable + "</b> " + type + " " + value + "</td>\
					<td><div class='actiones'></div><div class='clear'></div><a add-action='' style='font-size: 13px; color: #afafaf; '>Add action</a></td>\
				</tr>";
		$("#main_table tbody").append(data).sortable('refresh');
		$(".overlay").fadeOut();
		$(".variable-condition").removeClass("active").hide();
		encja = null;
		return false;
	});
	$(document).on('submit','form[name=action_add]',function(){
		var value = $(this).children("[name=variable_value]").val();
		$(this).children("[name=variable_value]").val('');
		
		var data = "<div>\
				<div class='tbl-1' style='border-right: 1px solid #afafaf'>\
					V: <b>" + $(".action-core a.active").text() + "</b>\
				</div>\
				<div class='tbl-2'>\
					" + $(".action2 a.active").text() + " " + value + "\
				</div><div class='clear'></div></div>";	
		$(encja).children().children(".actiones").append(data);
		
		$(".overlay").fadeOut();
		$(".actions").removeClass("active").hide();
		$(".action2 .hidden").hide();
		$(".action3").hide();
		$(".action-element a.active").removeClass("active");
		encja = null;	
		return false;
	});
});