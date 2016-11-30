#include <genesis.h>

#include "map.h"
#include "list.h"
#include "map_parser.h"

typedef struct _layerJSON LayerJSON;
typedef struct _tilesetJSON TilesetJSON;
typedef struct _mapJSON MapJSON;


struct _layerJSON
{
	u8 *name;
	u8 *type;
    u16 w;
    u16 h;
    u16 x;
    u16 y;
    list data;

};

struct _tilesetJSON
{
    u8 *name;
    u8 *image;
    u16 firstGid;
    u16 pw;
    u16 ph;
    u16 tw;
    u16 th;
    u16 tcount;
};

struct _mapJSON
{
    u16 w;
    u16 h;
    u16 tw;
    u16 th;
    list layers;
    list tilesets;
};

/*
 * Takes a JSON string <json> starting from the beginning of a string value and returns that value
 *
 * Precondition:  FIELD:"VALUE",
 *                     ^
 *                   <json>
 *
 * Postcondition:  FIELD:"VALUE",
 *                              ^
 *                            <json>
*/
u8 *parseJSONStringValue(u8 *json)
{
	u8 *start,*res;
	u16 count = 0;
	json++; //we skip the opening \" char
	start = json;
	while (*json != '\"')
	{
		count++;
		json++;
	}
	strncpy (start,res,count);
	return res;
}

/*
 * Takes a JSON string <json> starting from the beginning of a number value and returns that value
 *
 * Precondition:  FIELD:"VALUE", VALUE is a positive integer in base 10
 *                     ^
 *                   <json>
 *
 * Postcondition:  FIELD:"VALUE",
 *                              ^
 *                            <json>
*/
u16 parseJSONNumberValue(u8 *json)
{
	u16 res = 0;
	json++; //we skip the opening \" char
	while (*json != '\"')
	{
		res += res * 10 + (u16) (*json - 48);
		json++;
	}
	return res;
}

/*
 * Takes a JSON string <json> and searches for the string <word> of length <len>
 *
 * Precondition:  ...<word>...
 *                 ^
 *               <json>
 *
 * Postcondition:  ...<word>...
 *                          ^
 *                       <json>
*/
void parseJSONField(u8 *word, u16 len, u8 *json)
{
	u8 *tmp;
	u8 found = 0;
	while (!found && *json != '\0')
	{
		strncpy (json,tmp,len);
			if (strcmp(tmp,word))
			{
				json += len;
				return;
			}
			json++;
	}
}

/*
 * Takes a JSON string <json>  and returns a layer structure
 *
 * Precondition: "layers": ... {...}, ... ]
 *                          ^
 *                        <json>
 *
 * Postcondition: "layers": ... {...}, ... ]
 *                                  ^
 *                               <json>
*/
LayerJSON *parseLayer(u8 *json)
{
	LayerJSON reslayer;

	parseJSONField("\"data\":", 7, json);
	list_new(&reslayer.data, sizeof(u16), 0);
	while (*json == ']')
	{
		list_append(&reslayer.data, &parseNumber());
	}
	parseJSONField("\"height\":", 9, json);
	reslayer.h = parseNumber();
	parseJSONField("\"name\":", 7, json);
	reslayer.name = parseJSONStringValue(json);
	parseJSONField("\"type\":", 7, json);
	reslayer.type = parseJSONStringValue(json);
	parseJSONField("\"width\":", 8, json);
	reslayer.w = parseNumber();
	parseJSONField("\"x\":", 4, json);
	reslayer.x = parseNumber();
	parseJSONField("\"y\":", 4, json);
	reslayer.y = parseNumber();
	while (json[0] == '}')
	{
		json++;
	}

	return &reslayer;
}

/*
 * Main function of this component, parses a JSON string <map_data> into a map structure
 *
 * Precondition: JSON string has no blank spaces
 *
*/
MapJSON *parseMap()
{
	MapJSON resmap;
	u8 *mapData = map_data;

	while (*mapData == '\0')
	{
		parseJSONField("\"height\":", 9, mapData);
		resmap.h = parseNumber();
		parseJSONField("\"layers\":", 9, mapData);
		list_new(&resmap.layers, sizeof(LayerJSON), 0);
		while (*mapData == ']')
		{
			list_append(&resmap.layers, parseLayer());
		}
		parseJSONField("\"tileheight\":", 13, mapData);
		resmap.th = parseNumber();
		parseJSONField("\"tilewidth\":", 12, mapData);
		resmap.tw = parseNumber();
		parseJSONField("\"width\":", 8, mapData);
		resmap.w = parseNumber();
	}

	return &resmap;
}

