import axios from "axios";
import {ChangeUserRolesRequest} from "../interfaces/requests/ChangeUserRolesRequest.ts";
import {User} from "../interfaces/models/User.ts";
import {ApiResponse} from "../interfaces/responses/ApiResponse.ts";
import {API_BASE_URL} from "../constants.ts";

export const fetchUsers = async (): Promise<User[]> => {
    try {
        const response = await axios.get<ApiResponse<User>>(`${API_BASE_URL}/api/users`);
        const {result, errors} = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        return result;
    } catch (e) {
        console.error("Error fetching users:", e);
        throw e;
    }
};

export const fetchUserRoles = async (userId: string): Promise<string[]> => {
    try {
        const response = await axios.get<ApiResponse<string>>(`${API_BASE_URL}/api/users/${userId}/roles`);
        const {result, errors} = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        return result;
    } catch (e) {
        console.error("Error fetching users:", e);
        throw e;
    }
};

export const changeUserRoles = async (data: ChangeUserRolesRequest): Promise<void> => {
    try {
        const response = await axios.put(`${API_BASE_URL}/api/users/update-roles`, data);
        console.log("Roles updated successfully", response.data);
    } catch (error) {
        console.error("Error updating user roles:", error);
        throw error;
    }
};