
LineRerouting = function(link) {
	var rerouting = this;

	rerouting.link = link;
	rerouting.state = {};
}

LineRerouting.prototype = {

	startDragging: function() {
		
		var rerouting = this;
		var link = rerouting.link;
		var editor = link.editor;

		if (!editor.isEditMode()) {
			return;
		}

		var start_elem_id = link["startElementId"];
		var end_elem_id = link["endElementId"];

		var element_list = editor.getElements();
		var start_elem = element_list[start_elem_id];
		var end_elem = element_list[end_elem_id];

		var points = link.getPoints();
		link.line.listening(false);

//console.log("LINE_DRAGGING startElem "+start_elem_id,start_elem);
		state = {
				points: points.slice(),
				index: rerouting.getLineIndex(points),

				startElementObj: start_elem,
				endElementObj: end_elem,

				startElementSVG: start_elem.buildSVG(),
				endElementSVG: end_elem.buildSVG(),

				//direction: line_direction,
				dragLayer: editor.getLayer("DragLayer"),
			};

		rerouting.state = state;
	},

	dragging: function() {

		var rerouting = this;
		var link = rerouting.link;		

		var state = rerouting.state;
		var index = state.index;

		var new_points = rerouting.moveSegmentPoints(index);

		link.rerouteLine(new_points, state);
		if (_EDITOR.onLabelMoved) { // by SK dragging...
			if (link && (typeof link.compartments!='undefined') && (typeof link.compartments.compartments != 'undefined')) {
				var groupIndex = {};
				for (var i=0; i<link.compartments.compartments.length; i++) {
					var g = link.compartments.compartments[i];
					if (typeof groupIndex[g.placement.group._id] == 'undefined')
						groupIndex[g.placement.group._id] = 0;
					else
						groupIndex[g.placement.group._id]++;
					var dx = g.placement.group.children[groupIndex[g.placement.group._id]].attrs.x;
					var dy = g.placement.group.children[groupIndex[g.placement.group._id]].attrs.y;
					_EDITOR.onLabelMoved(g._id, g.placement.group.x()+dx-2,g.placement.group.y()+dy-4,g.placement.width,g.placement.group.children[groupIndex[g.placement.group._id]].textHeight);
				}
			}
		}

		rerouting.state.dragLayer.batchDraw();
	},

	finishDragging: function() {

		var rerouting = this;
		var link = rerouting.link;
		var editor = link.editor;

		link.line.listening(true);

		//refreshing the layer
		rerouting.state.dragLayer.draw();

		//reseting the state
		rerouting.state = {};

		var list = {boxes: [],
					lines: [{id: link._id, points: link.getPoints()}],
					deltaX: 0,
					deltaY: 0,
				};
	
		new Event(editor, "collectionPositionChanged", list);
	},

	moveSegmentPoints: function() {

		var rerouting = this;
		var link = rerouting.link;	
		var editor = link.editor;
		var state = rerouting.state;

		var mouse_state = editor.getMouseState();
		var mouse_x = mouse_state["mouseX"];
		var mouse_y = mouse_state["mouseY"];

		var new_points = state["points"].slice();
		rerouting.setNewSegmentPoints(new_points, state.index, mouse_x, mouse_y);
		return new_points;
	},

	setNewSegmentPoints: function(new_points, index, new_x, new_y) {

		if (new_points[index] == new_points[index + 2]) {
			new_points[index] = new_x;
			new_points[index + 2] = new_x;
		}

		else if (new_points[index + 1] == new_points[index + 3]) {
			new_points[index + 1] = new_y;
			new_points[index + 3] = new_y;
		}
	},

	getLineIndex: function() {

		var rerouting = this;
		var link = rerouting.link;
		var editor = link.editor;

		var mouse_state = editor.getMouseState();
		var mouse_x = mouse_state["mouseX"];
		var mouse_y = mouse_state["mouseY"];

		var points = link.getPoints();

		var delta = 5;
		var index;


		var x1, x2, y1, y2;
		for (var i=0;i<points.length-2;i=i+2) {

			//perpendicular
			if (points[i] == points[i+2]) {
				x1 = points[i] - delta;
				x2 = points[i] + delta;

				y1 = Math.min(points[i+1], points[i+3]);
				y2 = Math.max(points[i+1], points[i+3]);
			}
				
			//parallel
			else {
				x1 = Math.min(points[i], points[i+2]);
				x2 = Math.max(points[i], points[i+2]);

				y1 = points[i+1] - delta;
				y2 = points[i+3] + delta;
			}

			//computing the segment being moved
			if (x1 <= mouse_x && mouse_x <= x2 && y1 <= mouse_y && mouse_y <= y2) {
				index = i;
				break;
			}
		}

		return index;
	},
}

