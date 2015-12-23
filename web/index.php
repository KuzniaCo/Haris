<?php
	$input = array(
		"variables" => 
					array(
						array("name" => "radio_volume", "default" => "0")
					),
		"events" 	=> 
					array(
						array(
							"encja" => "Radio", 
							"condition" => "onStart", 
							"actions" => array(
											array("Variable" => "radio_volume", "action" => "Set", "value" => 0)
										)
						),
						array(
							"encja" => "Radio", 
							"condition" => "onStop", 
							"actions" => array(
											array("Variable" => "radio_volume", "action" => "Set", "value" => 0)
										)
						),							
						array(
							"encja" => "Radio", 
							"condition" => "onVolumeAdd", 
							"actions" => array(
											array("Variable" => "radio_volume", "action" => "Add", "value" => 1)
										)
						),	
						array(
							"encja" => "Radio", 
							"condition" => "onVolumeSub", 
							"actions" => array(
											array("Variable" => "radio_volume", "action" => "Sub", "value" => 1)
										)
						),
						array(
							"encja" => "System", 
							"condition" => "Every tick", 
							"actions" => array(
											//array("Variable" => "radio_volume", "action" => "Set", "value" => 0)
										)
						),					
						array(
							"encja" => "", 
							"condition" => "if",
							"variable" => "radio_volume",
							"end" => "<= 50",
							"actions" => array(
											array("GUID" => "1", "action" => "Blink")
										)
						),					
						array(
							"encja" => "", 
							"condition" => "else", 
							"actions" => array(
											array("GUID" => "1", "action" => "Turn On")
										)
						),					
					),
		"items"	 	=> 
					array(
						array(
							"GUID" => 1,
							"name" => "Lamp",
							"events" => 
								array(
									"onStart",
									"onStop"
								),
							"actions" =>
								array(
									"Turn On",
									"Turn Off",
									"Blink"
								)
						),
						array(
							"GUID" => 2,
							"name" => "Radio",
							"events" => 
								array(
									"onStart",
									"onChannelSwitch",
									"onVolumeAdd",
									"onVolumeSub",
									"onStop"
								),
							"actions" =>
								array(
									"Turn On",
									"Turn Off",
									"Blink"
								)
						)
					)						
	);
	
	// load system
	$input["items"][] = array(
		"GUID" => 0, 
		"name" => "System",
		"events" => array("onStart", "onStop", "Trigger once", "Every tick"),
		"actions" => array("Ping central", "Restart", "Turn off")
	);
	
	// Load config?
	foreach($input["events"] as $e) {
		if($e["condition"] == "if" || $e["condition"] == "elseif")
			$condition = $e["condition"].' <b>'.$e["variable"].'</b> '.$e["end"];
		else 
			$condition = $e["condition"];
		
		$data .= '
			<tr '.($e["condition"] == "if" || $e["condition"] == "elseif" || $e["condition"] == "else" ? 'class="can-sort"' : '').'>
				<td>'.$e["encja"].'</td>
				<td>'.$condition.'</td>
				<td>
					<div class="actiones">';
						foreach($e["actions"] as $a) {
							if(empty($a['action'])) 
								continue;
							$name = $value = '';
							if(isset($a['GUID']))
							{
								foreach($input["items"] as $i) {
									if($a['GUID'] == $i['GUID']) {
										$name = $i['name'];
										break;
									}
								}
								if(empty($name)) 
									continue;
								$value = $a['action'];
							}
							else if(isset($a['Variable']))
							{
								$name = 'V: <b>'.$a['Variable'].'</b>';
								$value = $a['action'].' '.$a['value'];
							}
							$data .= '<div>
								<div '.(isset($a['GUID']) ? 'data-guid="'.$a['GUID'].'"' : '').' class="tbl-1" style="border-right: 1px solid #afafaf">
									'.$name.'
								</div>
								<div class="tbl-2">
									'.$value.'
								</div>
								<div class="clear"></div>
							</div>';
						}
		$data .= '	</div>
					<a add-action="" style="font-size: 13px; color: #afafaf;">Add action</a>
				</td>
			</tr>
		';
	}
	$variables = '';
	foreach($input['variables'] as $v) {
		$variables .= '
			<tr>
				<td>'.$v['name'].'</td>
				<td>'.$v['default'].'</td>
			</tr>
		';
	}
?>
<!DOCTYPE html>
<html xml:lang="pl" lang="pl" xmlns="http://www.w3.org/1999/xhtml">
    <head>
		<title>Projekt Haris</title>
 		<script type="text/javascript" src="js/jquery.js"></script>
 		<script type="text/javascript" src="js/jquery-ui.js"></script>
		<script type="text/javascript" src="js/script.js"></script>
		
		<link rel="stylesheet" href="style/global.css" type="text/css" />	
	</head>
	
	<body>
		<div class="overlay"></div>
		<div class="main_width">
			<main>
				<h1>Project Haris</h1>
				<table width="100%" border="1" id="variable_table" class="<?php if(!$variables): ?>hidden<?php endif; ?>">
					<tr class="header">
						<td width="50%">Name</td>
						<td width="50%">Default</td>
					<tr>
					<?= $variables ?>
				</table>
				<table width="100%" border="1" id="main_table">
					<tr class="header">
						<td width="20%">Entity</td>
						<td width="20%">Condition</td>
						<td width="auto">Action</td>
					<tr>
					<?= $data ?>
				</table>
				<a data-generate="" class="button">Generate JSON</a>
				<textarea id="generated" class="hidden"></textarea>
				<div class="variable hidden main_width">
					<div class="right"><a close-div="">X</a></div>
					<form action="" method="" name="add_variable">
						Variable name:<br>
						<input type="text" name="variable_name"><br>
						<br>
						Variable default value:<br>
						<input type="text" name="variable_value"><br>
						<input type="submit" value="Add">
					</form>
				</div>
				<div class="variable-condition hidden main_width">
					<div class="right"><a close-div="">X</a></div>
					<form action="" method="" name="condition_variable">
						Variable:<br>
						<select name="variable_variable">
							<?php foreach($input['variables'] as $v): ?>
								<option value="<?= $v['name'] ?>"><?= $v['name'] ?></option>
							<?php endforeach; ?>
						</select><br>
						<br>
						Condition:<br>
						<select name="variable_condition">
							<option value="==">==</option>
							<option value="!=">!=</option>
							<option value="<="><=</option>
							<option value=">=">>=</option>
						</select><br>
						<br>
						Condition value:<br>
						<input type="text" name="variable_value"><br>
						<input type="submit" value="Add">
					</form>
				</div>
				<div class="if hidden main_width">
					<div class="right"><a close-div="">X</a></div>
					<ul>
						<?php if(count($input['variables'])): ?>
							<div class="guid-0 hidden">
								<li><a data-if="if">if</a></li>
								<li><a data-if="elseif">elseif</a></li>
								<li><a data-if="else">else</a></li>
							</div>
						<?php endif; ?>
						<?php foreach($input["items"] as $i): ?>
							<div class="guid-<?= $i['GUID']; ?> hidden">
								<?php foreach($i["events"] as $e): ?>
									<li><a data-if="<?= $e ?>"><?= $e ?></a></li>
								<?php endforeach; ?>
							</div>
						<?php endforeach; ?>
					</ul>
				</div>
				<div class="actions hidden main_width">
					<div class="right"><a close-div="">X</a></div>
					<div class="tbl-3 left">
						<?php if(count($input["items"]) || count($input['variables'])): ?>
							<ul class="action-element action-core">
								<?php foreach($input["items"] as $i): ?>
									<li>
										<a data-typ="add-action" data-guid="<?= $i['GUID']; ?>" data-type=""><?= $i['name']; ?></a>
									</li>
								<?php endforeach; ?>
								<?php foreach($input['variables'] as $v): ?>
									<li>
										<a data-typ="add-action" data-name="<?= $v['name']; ?>" data-type="variable"><?= $v['name']; ?></a>
									</li>
								<?php endforeach; ?>
							</ul>	
						<?php endif; ?>
					</div>
					<div class="tbl-3 left">
						<ul class="action2 action-element">
							<?php foreach($input["items"] as $i): ?>
								<div class="guid-<?= $i['GUID']; ?> hidden">
									<?php foreach($i["actions"] as $a): ?>
										<li><a data-action="<?= $a ?>"><?= $a ?></a></li>
									<?php endforeach; ?>
								</div>
							<?php endforeach; ?>
							<div class="variableact2 hidden">
								<li><a data-variable="set">Set</a></li>
								<li><a data-variable="add">Add</a></li>
								<li><a data-variable="sub">Sub</a></li>
								<li><a data-variable="div">Div</a></li>
								<li><a data-variable="mul">Mul</a></li>
							</div>
						</ul>
					</div>
					<div class="tbl-3 left">
						<div class="action3 hidden">
							<form action="" method="" name="action_add">
								Value:<br>
								<input type="text" name="variable_value"><br>
								<input type="submit" value="Add">
							</form>
						</div>
					</div>
					<div class="clear"></div>
				</div>				
			</main>
		</div>
		
		<footer>
			<ul class="main">
				<?php if(count($input['events'])): ?>
					<li>
						<a data-show=".elements">Add event</a>
						<ul class="elements hidden">
							<?php foreach($input["items"] as $e): ?>
								<li>
									<a data-typ="add-element" data-guid="<?= $e['GUID']; ?>" data-type="<?= $e['type'] ?>"><?= $e['name']; ?></a>
								</li>
							<?php endforeach; ?>
						</ul>
					</li>
				<?php endif; ?>		
				<li>
					<a data-typ="add-variable">Add variable</a>
				</li>
			</ul>		
		</footer>
	</body>
</html>