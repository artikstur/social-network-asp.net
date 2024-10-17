import axios from "axios";
import {ChangeUserRolesRequest} from "../interfaces/requests/ChangeUserRolesRequest.ts";
import {User} from "../interfaces/models/User.ts";
import {API_BASE_URL} from "../constants.ts";
import {LoginUserRequest} from "../interfaces/requests/LoginUserRequest.ts";
import {RegisterUserRequest} from "../interfaces/requests/RegisterUserRequest.ts";

export interface ApiResponse<T> {
    result: T;
    errors: string[];
    timeGenerated: string;
}

export const fetchUsers = async (): Promise<User[]> => {
    try {
        const response = await axios.get<ApiResponse<User[]>>(`${API_BASE_URL}/api/users`);
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
        const response = await axios.get<ApiResponse<string[]>>(`${API_BASE_URL}/api/users/${userId}/roles`);
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

export const loginUser = async (data: LoginUserRequest): Promise<string> => {
    console.log(data)
    try {
        const response = await axios.post<ApiResponse<LoginResponse>>(`${API_BASE_URL}/api/users/login`, data);

        const {result, errors} = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        return result.token;
    } catch (error) {
        console.error('Error logging in:',  );
        throw error;
    }
};

export const registerUser = async (data: RegisterUserRequest): Promise<string> => {
    console.log(data)
    try {
        const response = await axios.post<ApiResponse<string>>(`${API_BASE_URL}/api/users/register`, data);

        const {result, errors} = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        return result;
    } catch (error) {
        console.error('Error logging in:',  );
        throw error;
    }
};