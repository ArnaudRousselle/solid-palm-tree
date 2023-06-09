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
 * @interface AddBillingRequest
 */
export interface AddBillingRequest {
    /**
     * 
     * @type {string}
     * @memberof AddBillingRequest
     */
    portfolioId: string;
    /**
     * 
     * @type {DateOnly}
     * @memberof AddBillingRequest
     */
    date: DateOnly;
    /**
     * 
     * @type {string}
     * @memberof AddBillingRequest
     */
    name: string;
    /**
     * 
     * @type {number}
     * @memberof AddBillingRequest
     */
    amount: number;
    /**
     * 
     * @type {boolean}
     * @memberof AddBillingRequest
     */
    checked: boolean;
    /**
     * 
     * @type {string}
     * @memberof AddBillingRequest
     */
    comment: string;
    /**
     * 
     * @type {boolean}
     * @memberof AddBillingRequest
     */
    isArchived: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof AddBillingRequest
     */
    isSaving: boolean;
}

/**
 * Check if a given object implements the AddBillingRequest interface.
 */
export function instanceOfAddBillingRequest(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "portfolioId" in value;
    isInstance = isInstance && "date" in value;
    isInstance = isInstance && "name" in value;
    isInstance = isInstance && "amount" in value;
    isInstance = isInstance && "checked" in value;
    isInstance = isInstance && "comment" in value;
    isInstance = isInstance && "isArchived" in value;
    isInstance = isInstance && "isSaving" in value;

    return isInstance;
}

export function AddBillingRequestFromJSON(json: any): AddBillingRequest {
    return AddBillingRequestFromJSONTyped(json, false);
}

export function AddBillingRequestFromJSONTyped(json: any, ignoreDiscriminator: boolean): AddBillingRequest {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'portfolioId': json['portfolioId'],
        'date': DateOnlyFromJSON(json['date']),
        'name': json['name'],
        'amount': json['amount'],
        'checked': json['checked'],
        'comment': json['comment'],
        'isArchived': json['isArchived'],
        'isSaving': json['isSaving'],
    };
}

export function AddBillingRequestToJSON(value?: AddBillingRequest | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'portfolioId': value.portfolioId,
        'date': DateOnlyToJSON(value.date),
        'name': value.name,
        'amount': value.amount,
        'checked': value.checked,
        'comment': value.comment,
        'isArchived': value.isArchived,
        'isSaving': value.isSaving,
    };
}

