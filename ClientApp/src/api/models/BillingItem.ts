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

import { exists, mapValues } from '../runtime';
import type { DateOnly } from './DateOnly';
import {
    DateOnlyFromJSON,
    DateOnlyFromJSONTyped,
    DateOnlyToJSON,
} from './DateOnly';

/**
 * 
 * @export
 * @interface BillingItem
 */
export interface BillingItem {
    /**
     * 
     * @type {string}
     * @memberof BillingItem
     */
    readonly billingId: string;
    /**
     * 
     * @type {DateOnly}
     * @memberof BillingItem
     */
    date: DateOnly;
    /**
     * 
     * @type {string}
     * @memberof BillingItem
     */
    readonly label: string;
    /**
     * 
     * @type {number}
     * @memberof BillingItem
     */
    readonly amount: number;
    /**
     * 
     * @type {boolean}
     * @memberof BillingItem
     */
    readonly checked: boolean;
    /**
     * 
     * @type {string}
     * @memberof BillingItem
     */
    readonly comment: string;
    /**
     * 
     * @type {boolean}
     * @memberof BillingItem
     */
    readonly isArchived: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof BillingItem
     */
    readonly isSaving: boolean;
}

/**
 * Check if a given object implements the BillingItem interface.
 */
export function instanceOfBillingItem(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "billingId" in value;
    isInstance = isInstance && "date" in value;
    isInstance = isInstance && "label" in value;
    isInstance = isInstance && "amount" in value;
    isInstance = isInstance && "checked" in value;
    isInstance = isInstance && "comment" in value;
    isInstance = isInstance && "isArchived" in value;
    isInstance = isInstance && "isSaving" in value;

    return isInstance;
}

export function BillingItemFromJSON(json: any): BillingItem {
    return BillingItemFromJSONTyped(json, false);
}

export function BillingItemFromJSONTyped(json: any, ignoreDiscriminator: boolean): BillingItem {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'billingId': json['billingId'],
        'date': DateOnlyFromJSON(json['date']),
        'label': json['label'],
        'amount': json['amount'],
        'checked': json['checked'],
        'comment': json['comment'],
        'isArchived': json['isArchived'],
        'isSaving': json['isSaving'],
    };
}

export function BillingItemToJSON(value?: BillingItem | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'date': DateOnlyToJSON(value.date),
    };
}

