#include <genesis.h>

#include "list.h"

void list_new(list *list, int elementSize, freeFunction freeFn)
{
  assert(elementSize > 0);
  list->logicalLength = 0;
  list->elementSize = elementSize;
  list->head = list->tail = 0;
  list->freeFn = freeFn;
}

void list_destroy(list *list)
{
  listNode *current;
  while(list->head != 0) {
    current = list->head;
    list->head = current->next;

    if(list->freeFn) {
      list->freeFn(current->data);
    }

    free(current->data);
    free(current);
  }
}

void list_prepend(list *list, void *element)
{
  listNode *node = malloc(sizeof(listNode));
  node->data = malloc(list->elementSize);
  memcpy(node->data, element, list->elementSize);

  node->next = list->head;
  list->head = node;

  // first node?
  if(!list->tail) {
    list->tail = list->head;
  }

  list->logicalLength++;
}

void list_append(list *list, void *element)
{
  listNode *node = malloc(sizeof(listNode));
  node->data = malloc(list->elementSize);
  node->next = 0;

  memcpy(node->data, element, list->elementSize);

  if(list->logicalLength == 0) {
    list->head = list->tail = node;
  } else {
    list->tail->next = node;
    list->tail = node;
  }

  list->logicalLength++;
}

void list_for_each(list *list, listIterator iterator)
{
  assert(iterator != 0);

  listNode *node = list->head;
  bool result = TRUE;
  while(node != 0 && result) {
    result = iterator(node->data);
    node = node->next;
  }
}

void list_head(list *list, void *element, bool removeFromList)
{
  assert(list->head != 0);

  listNode *node = list->head;
  memcpy(element, node->data, list->elementSize);

  if(removeFromList) {
    list->head = node->next;
    list->logicalLength--;

    MEM_free(node->data);
    MEM_free(node);
  }
}

void list_tail(list *list, void *element)
{
  assert(list->tail != 0);
  listNode *node = list->tail;
  memcpy(element, node->data, list->elementSize);
}

int list_size(list *list)
{
  return list->logicalLength;
}
