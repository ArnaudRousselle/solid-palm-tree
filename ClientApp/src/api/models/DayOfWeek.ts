/* tslint:disable */
/* eslint-disable */
/**
 * MyPersonalAccounting
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


/**
 * 
 * @export
 */
export const DayOfWeek = {
    NUMBER_0: 0,
    NUMBER_1: 1,
    NUMBER_2: 2,
    NUMBER_3: 3,
    NUMBER_4: 4,
    NUMBER_5: 5,
    NUMBER_6: 6
} as const;
export type DayOfWeek = typeof DayOfWeek[keyof typeof DayOfWeek];


export function DayOfWeekFromJSON(json: any): DayOfWeek {
    return DayOfWeekFromJSONTyped(json, false);
}

export function DayOfWeekFromJSONTyped(json: any, ignoreDiscriminator: boolean): DayOfWeek {
    return json as DayOfWeek;
}

export function DayOfWeekToJSON(value?: DayOfWeek | null): any {
    return value as any;
}

