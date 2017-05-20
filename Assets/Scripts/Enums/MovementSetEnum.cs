/*
 Classification:
	- Number of steps
		1 / 2 / 3 / 4
		
	- With/without Attack
		ATK / DEF
		
	- Limited by the battlefield's size
		- HRZ (horizontal): Shift 3 or more 'horz'
		- VRT (vertical): Shift 3 or more 'vert'
*/
public enum MovementSetEnum {
	_1_ATK,
	_1_DEF,
	_2_ATK,
	_2_DEF,
	_3_ATK,
	_3_DEF,
	_3_ATK_HRZ,
	_3_DEF_HRZ,
	_3_ATK_VRT,
	_3_DEF_VRT,
	_4_ATK_HRZ,
	_4_DEF_HRZ,
	_4_ATK_VRT,
	_4_DEF_VRT,
}
