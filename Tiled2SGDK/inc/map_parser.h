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
u8 *parseJSONStringValue(u8 *json);

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
u16 parseJSONNumberValue(u8 *json);

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
void parseJSONField(u8 *word, u16 len, u8 *json);

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
LayerJSON *parseLayer(u8 *json);

/*
 * Main function of this component, parses a JSON string <map_data> into a map structure
 *
 * Precondition: JSON string has no blank spaces
 *
*/
MapJSON *parseMap();
